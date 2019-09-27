using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCCCompact.Models
{
    class SizeCalcuter
    {
        Node largestNode;
        private readonly double bounderMLT = 0.7;
        private readonly double firstInternallRadius = 30;

        public void Process(Node largestNode)
        {
            SetLargestNode(largestNode);
            Calcute(largestNode);
        }

        private void SetLargestNode(Node largestNode)
        {
            this.largestNode = largestNode;
        }

        private void Calcute(Node node)
        {
            SetSizes(node);
            double sumOfAllChildsSizes = SumOfChildrenSizes(node);
            if (sumOfAllChildsSizes == 0)
            {
                node.externallRadius = node.internallRadius;
                return;
            }
            Dictionary<Vertex, double> vertex_share = new Dictionary<Vertex, double>();
            double maxExternallRadius = node.internallRadius;
            foreach (Vertex vertex in node.adjacenty_vertex_nodes.Keys)
            {
                vertex_share[vertex] = GetSumOfChildrenSizes(vertex,node);
            }

            foreach (Vertex vertex in vertex_share.Keys)
            {
                double allVertexAngel = (vertex_share[vertex] / sumOfAllChildsSizes) * Math.PI * 2;
                if (allVertexAngel > Math.PI * 2 / 3)
                {
                    allVertexAngel = Math.PI * 2 / 3;
                }
                foreach (Node child in node.adjacenty_vertex_nodes[vertex])
                {
                    if (child == node.parent)
                    {
                        continue;
                    }
                    double angel = (child.externallRadius / vertex_share[vertex]) * allVertexAngel;
                    if (angel != 0)
                    {
                        child.parentAngleShare = angel;
                        angel /= 2;
                        double minDistanceToCenter = Math.Abs(child.externallRadius / Math.Sin(angel));
                        if (minDistanceToCenter < child.externallRadius + node.internallRadius)
                        {
                            minDistanceToCenter = child.externallRadius + node.internallRadius;
                        }
                        if (maxExternallRadius < minDistanceToCenter + child.externallRadius)
                        {
                            maxExternallRadius = minDistanceToCenter + child.externallRadius;
                        }
                        child.connectionLenght = minDistanceToCenter;
                    }
                }
            }
            node.externallRadius = maxExternallRadius;
        }


        public double GetSumOfChildrenSizes(Vertex vertex,Node node)
        { 
            double sumOfSize = 0;

            foreach (Node child in node.adjacenty_vertex_nodes[vertex])
            {
                if (child == node.parent)
                {
                    continue;
                }
                sumOfSize += child.externallRadius;
            }
            return sumOfSize;
        }


        private void SetSizes(Node node)
        {
            SetInternallRadius(node);
            foreach (Node child in node.children)
            {
                Calcute(child);
            }
        }

        public double SumOfChildrenSizes(Node node)
        {
            double sum = 0;

            foreach (Node child in node.children)
            {
                if (child.externallRadius == 0)
                {
                    Calcute(child);
                }
                sum += child.externallRadius;
            }
            return sum;
        }

        private void SetInternallRadius(Node node)
        {
            if (node.GetVertices().Count == 1)
            {
                node.internallRadius = firstInternallRadius;
            }
            else
            {
                node.internallRadius = node.GetVertices().Count * firstInternallRadius;
            }
        }

    }
}
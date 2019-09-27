
@section scripts{
    <script>
        function compact() {
        const queryText = $('#text').val();
        $.ajax({
            url: '/query/compact',
        method: 'post',
            data: {query: queryText }
        }).done((result) => {
            drawGraph(result);
        });
}
    function randomGraphCompact() {

        const v = $('#V').val();
        const e = $('#E').val();
        var queryText = v + "-" + e;
        $.ajax({
            url: '/query/randomGraph',
        method: 'post',
            data: {query: queryText }
        }).done((result) => {
            drawGraph(result);
        });
}

    function drawGraph(result) {
        var g = {nodes: [], edges: [] };
        $.each(result.locations, (index, value) => {
            g.nodes.push({
                id: 'n' + index,
                attributes: {
                    x: value.x,
                    y: value.y,
                    text: 'N' + index,
                    radius: 3
                }
            });
        });
        $.each(result.edges, (index, value) => {
            g.edges.push({
                id: 'e' + index,
                source: 'n' + value.v,
                target: 'n' + value.u,
                attributes: { text: 'edge' + index }
            });
        });
        $('#text').html('');
        var ogma = new Ogma({
            graph: g,
        container: 'graph-container'
    });
}
    </script>
}







<script>
    function drawRandomGraph() {
        var element = document.getElementById('graph-container');
    var positionInfo = element.getBoundingClientRect();
    var height = positionInfo.height;
    var width = positionInfo.width;
    var v = $('#V').val();
    var e = $('#E').val();
    v = parseInt(v);
    e = parseInt(e);
        function randomGraph(N, E) {
            var g = {nodes: [], edges: [] };
            for (var i = 0; i < N; i++) {
        g.nodes.push({
            id: 'n' + i,
            attributes: {
                x: Math.random() * width,
                y: Math.random() * height,
                text: 'n' + i,
                radius: 100
            }
        });
    }
            for (var i = 0; i < E; i++) {
        g.edges.push({
            id: 'e' + i,
            source: 'n' + (Math.random() * N | 0),
            target: 'n' + (Math.random() * N | 0),
            attributes: { text: 'edge' + i }
        });
    }
    return g;
}
var g = randomGraph(v, e);
        var ogma = new Ogma({
        graph: g,
    container: 'graph-container'
});
}


</script>
export function ShowXY(x, y)
{
    var xArray = x;
    var yArray = y;

    var config = { responsive: true }
    var data = [{ x: xArray, y: yArray, mode: "lines" }];
    var layout = {
        title: 'Demo',
        xaxis: {
            title: 'x',
            titlefont: {
                family: 'Arial, sans-serif',
                size: 18,
                color: 'lightgrey'
            },
            showticklabels: true,
            tickangle: 'auto',
            tickfont: {
                family: 'Old Standard TT, serif',
                size: 14,
                color: 'black'
            },
            exponentformat: 'e',
            showexponent: 'all'
        },
        yaxis: {
            title: 'y',
            titlefont: {
                family: 'Arial, sans-serif',
                size: 18,
                color: 'lightgrey'
            },
            showticklabels: true,
            tickangle: 45,
            tickfont: {
                family: 'Old Standard TT, serif',
                size: 14,
                color: 'black'
            },
            exponentformat: 'e',
            showexponent: 'all'
        }
    };

    Plotly.newPlot("demoPlot", data, layout, config);
    console.info("show plot");
}
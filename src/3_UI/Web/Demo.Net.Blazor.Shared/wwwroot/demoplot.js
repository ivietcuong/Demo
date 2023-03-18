export function ShowXY(x, y)
{
    var xArray = x;
    var yArray = y;

    var config = { responsive: true }
    var data = [{ x: xArray, y: yArray, mode: "lines" }];
    var layout = {
        xaxis: {
            title: 'x',            
            tickangle: 'auto',            
            exponentformat: 'e',
            showexponent: 'all',
            showticklabels: true
        },
        yaxis: {
            title: 'y',            
            tickangle: 0,            
            exponentformat: 'e',
            showexponent: 'all',
            showticklabels: true
        }
    };

    Plotly.newPlot("demoPlot", data, layout, config);
    console.info("show plot");
}
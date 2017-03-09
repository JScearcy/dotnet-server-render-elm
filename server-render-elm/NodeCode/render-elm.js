let jsdom = require('jsdom');
let fs = require("fs");
let elmCode = fs.readFileSync("./wwwroot/js/Main.js", "utf-8");


module.exports = {
    renderElmFromJS: function (cb, flags) {
        jsdom.env({
            html: '<div id="elm-code"></div>',
            src: [ elmCode ],
            done: function (err, window) {
                var node = window.document.getElementById('elm-code');
                if (flags) {
                    var app = window.Elm.Main.embed(node, JSON.parse(flags));
                } else {
                    var app = window.Elm.Main.embed(node);
                }
                setTimeout(() => cb(err, window.document.getElementById('elm-code').innerHTML), 0);
            }
        });
    }
}
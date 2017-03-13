let eap = require('elm-aspnet-prerendering');
let path = require('path');
let fs = require('fs');

module.exports = {
    prerender: function (done, flags) {
        let jsPath = path.join(__dirname, '../wwwroot/js/Main.js');
        fs.readFile(jsPath, (err, data) => {
            let config = {
                flags,
                src: data,
                done
            };

            eap.renderElmFromJS(config);
        });
    }
}
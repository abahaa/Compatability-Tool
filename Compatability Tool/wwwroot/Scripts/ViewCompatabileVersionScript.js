//import json from '../../appsettings.json'

//export default {
//    data() {
//        return {
//            myJson: json
//        }
//    }
//}

//console.log(json)

String.prototype.format = function () {
    var args = [].slice.call(arguments);
    return this.replace(/(\{\d+\})/g, function (a) {
        return args[+(a.substr(1, a.length - 2)) || 0];
    });
};

var app = new Vue({
    el: '#view',
    data: {
        show: false,
        options: [
            {
                'id': 1,
                'name': 'AMAP'
            },
            {
                'id': 2,
                'name': 'ASRA'
            },
            {
                'id': 3,
                'name': 'IMAP'
            },
            {
                'id': 4,
                'name': 'ISRA'
            },
            {
                'id': 5,
                'name': 'WebAPI'
            }],
        selectedComp1: '',
        selectedComp2: '',
        releases: [],
        selectID: '',
        MatchedReleases: ''
    },
    methods: {

        GetReleases() {
            app.show = false
            if (app.selectedComp1 != null) {
                var url = "/api/getReleases/" + app.selectedComp1;
                axios({ method: "GET", "url": url, "headers": { "content-type": "application/json" } }).
                    then(result => {
                        app.releases = result.data;
                        app.show = true;
                    }, error => {
                        console.error(error);
                        alert('There is An error .. check the console')
                        app.show = false
                    });
                
            }
            else {
                alert('Something Wrong')
                app.show=false
            }
        }, GetMatchedVersions() {
            app.MatchedReleases = ""
            if (app.selectedComp1 == '' || app.selectedComp2 == '' || app.selectID == '') {
                app.MatchedReleases = '<p style="color:red;">Please Check You Had selected all the fields then <span style="color:green;">Test</span></p>'
                return;
            }
            else if (app.selectedComp1 == app.selectedComp2) {
                app.MatchedReleases = '<p style="color:red;">You Can not Test Compatability between the same Devices</p>'
                return;
            }
           
            var url = "/api/get/" + app.selectedComp1 + "/" + app.selectedComp2 + "/" + app.selectID;

            axios({ method: "GET", "url": url, "headers": { "content-type": "application/json" } }).
                then(result => {
                    var response = result.data;
                    var MatchedReleases = ""
                    console.log(response)

                    var CheckComponentName = function (ComponentID) {
                        for (var i = 0; i < app.options.length; i++) {
                            if (app.options[i].id == ComponentID) {
                                return app.options[i].name;
                            }
                        }
                        return 0;
                    }

                    var i = 0;
                    for (i; i < response.length; i++) {
                        var MasterComponentName = CheckComponentName(response[i].masterComponentId)
                        var DependantComponentName = CheckComponentName(response[i].dependantComponentId)
                        var str = "<p> {0} Version:{1} is Compatabile with  {2} at versions from version:{3} to version:{4}</p>";
                        MatchedReleases += (str.format(MasterComponentName, response[i].masterVersion, DependantComponentName, response[i].minDependVersion, response[i].maxDependVersion));
                    }
                    app.MatchedReleases = MatchedReleases;
                }).catch((error) => {
                    app.MatchedReleases = '<p style="color:red">{0}</p>'.format(error.response.data);
                });
        }
    }
});
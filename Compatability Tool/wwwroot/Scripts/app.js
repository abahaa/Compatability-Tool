//export default {
//    methods: {
//        loadTextFromFile(ev) {
//            const file = ev.target.files[0];
//            const reader = new FileReader();

//            reader.onload = e => this.$emit("load", e.target.result);
//            reader.readAsText(file);
//        }
//    }
//};

const TestRelease = {
    template: '<div v-html="testRelease.html"></div>' }
const routes = [
    { path: '/TestRelease', component: TestRelease },
]
const router = new VueRouter({
    routes // short for `routes: routes`
})

var app = new Vue({
    //router,
    el: '#app',
    data: {
        json: '',
        response: ''
    },
    methods: {
        onFileChange(e) {
            var files = e.target.files || e.dataTransfer.files;
            if (!files.length)
                return;
            this.ParseJSON(files[0]);
        },
        ParseJSON(file) {
            var reader = new FileReader();

            reader.onloadend = function () {
                app.json = reader.result;
                console.log(app.json);
            }
            reader.readAsText(file);
        },
        sendData() {
            app.response = ''
            var obj = JSON.parse(app.json);
            console.log(obj)
            var url = "/api/post";
            axios({ method: "POST", "url": url, "data": obj, "headers": { "content-type": "application/json" } }).
                then(result => {
                app.response = result.data;
            }, error => {
                console.error(error);
                app.response = error.response.data
            });
        }
    }
});
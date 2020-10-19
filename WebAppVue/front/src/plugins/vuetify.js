import Vue from 'vue';
import Vuetify from 'vuetify/lib';
import "../../node_modules/vuetify/dist/vuetify.min.css";
import "../../node_modules/material-design-icons-iconfont/dist/material-design-icons.css";
import "../../node_modules/@fortawesome/fontawesome-free/css/all.css";
// import "../../node_modules/@mdi/font/css/materialdesignicons.css";

Vue.use(Vuetify);

export default new Vuetify({
    icons: {
        iconfont: 'fa',
        // iconfont: 'mdi',
        // iconfont: [ 'fa', 'mdi'],
        // iconfont: "faSvg",
        // iconfont: "mdiSvg"
    },
});

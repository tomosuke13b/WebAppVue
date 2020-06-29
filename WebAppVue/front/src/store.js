import Vue from "vue";
import Vuex from "vuex";
import item from "./models/test/item";
import list from "./models/test/list";

Vue.use(Vuex);

export default new Vuex.Store({
    modules: {
        item,
        list,
    },
});

import Vue from "vue";
import Vuex from "vuex";
import item from "./models/node/item";
import list from "./models/node/list";

Vue.use(Vuex);

export default new Vuex.Store({
    modules: {
        item,
        list,
    },
});

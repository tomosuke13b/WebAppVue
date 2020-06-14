import api from "@/models/test/api.js";

const state = {
    test1: "HelloVue 1!!!!",
}

const getters = {
    getTest1: state => {
        return state.test1;
    }
}

const actions = {
    testAction({ commit }, id) {
        console.log("testAction");
        api.testApi(id, (item) => {
            commit("setTest1", item);
        });
    }
};

const mutations = {
    setTest1(state, value) {
        state.test1 = value;
    },
};

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
};
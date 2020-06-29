import api from "@/models/test/list/api.js";

const state = {
    items: []
}

const getters = {
    getItems: state => { 
        return state.items;
    }
}

const actions = {
    load({ commit }) {
        api.gets()
            .then((items) => {
                commit("setItems", items);
            });
    },
};

const mutations = {
    setItems(state, value) {
        state.items = value;
    },
};

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
};

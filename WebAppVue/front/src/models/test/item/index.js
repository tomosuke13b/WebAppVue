import api from "@/models/test/item/api.js";

const state = {
    item: {
        index: -1,
        name: null,
        description: null,
        timeStamp: null
    },
}

const getters = {
    getName: state => { 
        return state.item.name;
    },
    getDescription: state => {
        return state.item.description;
    },
    getTimeStamp: state => {
        return state.item.timeStamp;
    },
}

const actions = {
    createItem({ commit }) { 
        commit("initItem");
        commit("setName", null);
        commit("setDescription", null);
    },
    loadItem({ commit }, id) {
        commit("initItem");
        api.get(id)
            .then((item) => {
                commit("setIndex", item.index);
                commit("setName", item.name);
                commit("setDescription", item.description);
                commit("setTimeStamp", item.timeStamp);
            });
    },
    async saveItem({ state, commit }) {
        commit("setTimeStamp", new Date());
        await api.post(state.item)
            .then(() => {
                commit("initItem");
            });
    },
    async updateItem({ state, commit}, id) {
        await api.put(id, state.item)
            .then(() => {
                commit("initItem");
            });
    },
    async deleteItem({ commit }, id) {
        await api.delete(id)
            .then(() => {
                commit("initItem");
            });
    },
};

const mutations = {
    initItem(state) {
        state.item = {
            index: -1,
            name: null,
            description: null,
            timeStamp: null
        };
    },
    setIndex(state, value) {
        state.item.index = value;
    },
    setName(state, value) {
        state.item.name = value;
    },
    setDescription(state, value) {
        state.item.description = value;
    },
    setTimeStamp(state, value) {
        state.item.timeStamp = value;
    },
};

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
};

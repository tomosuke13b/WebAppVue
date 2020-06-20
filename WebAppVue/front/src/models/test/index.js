import api from "@/models/test/api.js";

const state = {
    test1: "HelloVue 1!!!!",
    isView: true,
    item: {
        index: -1,
        name: null,
        description: null,
        timeStamp: null
    },
    items: []
}

const getters = {
    getTest1: state => {
        return state.test1;
    },
    getIsView: state => {
        return state.isView;
    },
    getName: state => { 
        return state.item.name;
    },
    getDescription: state => {
        return state.item.description;
    },
    getTimeStamp: state => {
        return state.item.timeStamp;
    },
    getItems: state => { 
        return state.items;
    }
}

const actions = {
    testAction({ commit }, id) {
        console.log("testAction");
        api.testApi(id, (item) => {
            commit("setTest1", item);
        });
    },
    createItem({ commit }) { 
        commit("initItem");
        commit("setName", null);
        commit("setDescription", null);
    },
    loadItem({ state, commit }, index) {
        commit("initItem");
        state.items.forEach(item => {
            if (item.index == index) {
                commit("setIndex", item.index);
                commit("setName", item.name);
                commit("setDescription", item.description);
                commit("setTimeStamp", item.timeStamp);
                return;
            }
        });
    },
    saveItem({ commit }) {
        commit("setTimeStamp", new Date());
        commit("appendItem");
        commit("initItem");
    },
    updateItem({ commit }) {
        commit("updateItem");
        commit("initItem");
    },
};

const mutations = {
    setTest1(state, value) {
        state.test1 = value;
    },
    setIsView(state, value) {
        state.isView = value;
    },
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
    appendItem(state) { 
        state.item.index = state.items.length;
        state.items.push(state.item);
    },
    updateItem(state) {
        state.items.forEach(item => {
            if (item.index == state.item.index) {
                item.name = state.item.name;
                item.description = state.item.description;
                return;
            }
        });
    }
};

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
};

import api from "../api.js";
import tasks from "@/lib/tasks.js";

const state = {
    item: {
        index: -1,
        name: null,
        description: null,
        timeStamp: null,
        imageIds: []
    },
    images: []
}

const getters = {
    name: state => { 
        return state.item.name;
    },
    description: state => {
        return state.item.description;
    },
    timeStamp: state => {
        return state.item.timeStamp;
    },
    imageIds: state => {
        return state.item.imageIds;
    },
    images: state => {
        return state.images;
    },
    isImage: state => {
        return (!state.images ? false : 0 < state.images.length);
    },
}

const actions = {
    createItem({ commit }) { 
        commit("initItem");
        commit("setName", null);
        commit("setDescription", null);
        commit("setImageIds", []);
        commit("setImages", []);
    },
    loadItem({ commit, dispatch }, id) {
        commit("initItem");
        api.get(id)
            .then((item) => {
                commit("setIndex", item.index);
                commit("setName", item.name);
                commit("setDescription", item.description);
                commit("setTimeStamp", item.timeStamp);
                commit("setImages", item.images);
                dispatch("loadImage", item.imageIds);
            });
    },
    loadImage({ commit }, ids) {
        commit("setImages", []);
        if(!ids) return;
        Array.from(ids).forEach(id => {
            api.getImage(id)
                .then((image) => {
                    if(!image) return;
                    commit("addImage", { id: image.id, contentType: image.contentType, data : image.data});
                });
        });
    },
    async saveImage({ state, commit }) {
        commit("setTimeStamp", new Date());

        tasks.init();

        Array.from(state.images).forEach(image => {
            tasks.register(
                async () => {
                    await api.postImage(image).then((result) => {
                        commit("addImageId", result.id);
                    });
                }
            );
        });

        await tasks.exec();
    },
    async saveItem({ state, commit }) {
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
    setImageIds(state, imageIds) {
        state.item.imageIds = imageIds;
    },
    setImages(state, images) {
        state.images = images;
    },
    addImageIds(state, imageIds) {
        if (!state.item.imageIds) state.item.imageIds = [];
        Array.from(imageIds).forEach(imageId => {
            state.item.imageIds.push(imageId);
        });
    },
    addImageId(state, imageId) {
        if (!state.item.imageIds) state.item.imageIds = [];
        state.item.imageIds.push(imageId);
    },
    addImages(state, imageSources) {
        if (!state.images) state.images = [];
        Array.from(imageSources).forEach(imageSource => {
            state.images.push({ id: imageSource.id, preId: state.images.length + 1, imageSource: imageSource.contentType, data: imageSource.data });
        });
    },
    addImage(state, { id, contentType, data }) {
        if (!state.images) state.images = [];
        state.images.push({ id: id, preId: state.images.length + 1, contentType: contentType, data: data });
    }
};

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
};

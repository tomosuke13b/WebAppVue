import api from "../api.js";
import tasks from "@/lib/tasks.js";
import settings from "@/settings.js";

const state = {
    items: []
}

const getters = {
    items: state => { 
        return state.items;
    },
    image: state => id => {
        let index = state.items.findIndex(item => item.id == id);
        if (index === -1) return null;
        return state.items[index].image;
    },
    blankImage: state => id => {
        let index = state.items.findIndex(item => item.id == id);
        if (index === -1) return null;
        return state.items[index].blankImage;
    },
    isImageLoaded: state => id => {
        let index = state.items.findIndex(item => item.id == id);
        if (index === -1) return null;
        return state.items[index].isImageLoaded;

    }
}

const actions = {
    load({ commit, dispatch }) {
        tasks.init();

        api.gets()
            .then((items) => {
                commit("setItems", items);
                items.forEach(item => {
                    commit("setIsImageLoaded", { namesId:item.id, isImageLoaded: false });
                    if(!item.imageIds || item.imageIds.length == 0) {
                        commit("setBlankImage", { namesId:item.id });
                        commit("setIsImageLoaded", { namesId:item.id, isImageLoaded: true });
                        return;
                    }
                    tasks.register(
                        async () => {
                            dispatch("loadImage", 
                            { 
                                namesId: item.id, 
                                imageId: Number(item.imageIds[0])
                            });
                        }
                    );
                });
                tasks.exec();
            });
    },
    loadImage( { commit }, {namesId , imageId} ) {
        api.getImage(imageId)
            .then((image) => {
                commit("setImage", { namesId:namesId, image:image });
                commit("setIsImageLoaded", { namesId:namesId, isImageLoaded: true });
            });
    }
};

const mutations = {
    setItems(state, value) {
        state.items = value;
    },
    setImage(state, { namesId, image }) {
        let index = state.items.findIndex(item => item.id == namesId);
        if (index === -1) return null;
        state.items[index].image = image;
    },
    setBlankImage(state, { namesId }) {
        let index = state.items.findIndex(item => item.id == namesId);
        if (index === -1) return null;
        state.items[index].blankImage = settings.IMAGE_NO_IMAGE;
    },
    setIsImageLoaded(state, { namesId, isImageLoaded }) {
        let index = state.items.findIndex(item => item.id == namesId);
        if (index === -1) return null;
        state.items[index].isImageLoaded = isImageLoaded;
    }
};

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
};

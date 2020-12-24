import Vue from "vue";
import VueRouter from "vue-router";

import List from "@/components/content/List.vue";
import Home from "@/components/content/Home.vue";
import Info from "@/components/content/Info.vue";

import New from "@/components/content/item/New.vue";
import Detail from "@/components/content/item/Detail.vue";

Vue.use(VueRouter);

const routes = [

    {
        path: "/",
        name: "home",
        component: Home,
    },
    {
        path: "/info",
        name: "info",
        component: Info,
    },
    {
        path: "/list",
        name: "list",
        component: List
    },
    {
        path: "/item/:id",
        name: "detail",
        component: Detail,
        props(route) {
            const props = { ...route.params };
            props.id = Number(props.id);
            props.isView = (!props.isView) ? false : props.isView.toString() === "true";
            return props;
        }
    },
    {
        path: "/new",
        name: "new",
        component: New
    }
];

const router = new VueRouter({
    mode: "history",
    routes,
    scrollBehavior (to, from, savedPosition) {
        console.log(to);
        console.log(from);
        console.log(savedPosition);
        return { x: 0, y: 0 }
    }
});

export default router;

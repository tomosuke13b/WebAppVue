import Vue from "vue";
import VueRouter from "vue-router";
import TestList from "@/components/TestList.vue";
import TestNew from "@/components/TestNew.vue";
import TestDetail from "@/components/TestDetail.vue";
import TestHome from "@/components/TestHome.vue";
import TestInfo from "@/components/TestInfo.vue";

Vue.use(VueRouter);

const routes = [

    {
        path: "/",
        name: "testHome",
        component: TestHome,
    },
    {
        path: "/test/info",
        name: "testInfo",
        component: TestInfo,
    },
    {
        path: "/test/list",
        name: "testList",
        component: TestList
    },
    {
        path: "/test/new",
        name: "testNew",
        component: TestNew
    },
    {
        path: "/test/list/:id",
        name: "testDetail",
        component: TestDetail,
        props(route) {
            const props = { ...route.params };
            props.id = Number(props.id);
            props.isView = (!props.isView) ? false : props.isView.toString() === "true";
            return props;
        }
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

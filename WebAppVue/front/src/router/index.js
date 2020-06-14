import Vue from "vue";
import VueRouter from "vue-router";
import TestList from "@/components/TestList.vue";
import TestNew from "@/components/TestNew.vue";
import TestDetail from "@/components/TestDetail.vue";
import TestHome from "@/components/TestHome.vue";

Vue.use(VueRouter);

const routes = [

    {
        path: "/",
        name: "testHome",
        component: TestHome,
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
        component: TestDetail
    }
];

const router = new VueRouter({
    mode: "history",
    routes
});

export default router;

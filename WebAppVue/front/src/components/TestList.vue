<template>
    <v-container>
        <v-row class="text-center">
            <v-col cols="12">
                <v-col class="mb-4">
                    <h1 class="display-2 font-weight-bold mb-3">
                        一覧
                    </h1>
                </v-col>
                <v-col>
                    <v-btn small color="primary" to="/">Home</v-btn>
                </v-col>
                <v-col>
                    <v-btn small color="primary" to="/test/list/1">Detail</v-btn>
                </v-col>
                <v-col>
                    <v-btn small color="primary" to="/test/new">New</v-btn>
                </v-col>
                <v-col>
                    <v-list>
                        <v-list-item
                            v-for="(item, index) in items"
                            :key="index"
                        >
                            <v-list-item-content>
                                <v-list-item-title v-text="item.index"></v-list-item-title>
                            </v-list-item-content>
                            <v-list-item-content>
                                <v-list-item-title v-text="item.name"></v-list-item-title>
                            </v-list-item-content>
                            <v-list-item-content>
                                <v-list-item-title v-text="item.timeStamp"></v-list-item-title>
                            </v-list-item-content>
                            <v-list-item-action>
                                <v-btn small color="primary" @click="onEdit(item.index)"  >編集</v-btn>
                            </v-list-item-action>
                            <v-list-item-action>
                                <v-btn small color="primary" @click="onView(item.index)" >参照</v-btn>
                            </v-list-item-action>

                        </v-list-item>
                    </v-list>
                </v-col>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
    export default {
        name: "TestList",

        data: () => ({
        }),
        computed: {
            items() {
                return this.$store.getters["test/getItems"];
            }
        },
        mounted() {
        },
        methods: {
            onEdit(index) {
                if(index === undefined || index == -1) return;
                this.$store.commit("test/setIsView", false);
                this.$router.push("/test/list/" + index);
            },
            onView(index) {
                if(index === undefined || index == -1) return;
                this.$store.commit("test/setIsView", true);
                this.$router.push("/test/list/" + index);
            }
        }
    }
</script>

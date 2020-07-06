<template>
    <v-container>
        <v-btn class="mx-2" absolute left fab dark color="indigo" to="/">
            <v-icon dark>home</v-icon>
        </v-btn>
        <v-btn class="mx-2" absolute right fab dark color="indigo" to="/test/new">
            <v-icon dark>mdi-plus</v-icon>
        </v-btn>
        <v-row class="text-center">
            <v-col cols="12">
                <v-col class="mb-4">
                    <h1 class="display-2 font-weight-bold mb-3">
                        一覧
                    </h1>
                </v-col>
                <v-col>
                    <v-list>
                        <v-list-item
                            v-for="(item, index) in items"
                            :key="index"
                        >
                            <v-list-item-content>
                                <v-list-item-title v-text="item.name"></v-list-item-title>
                            </v-list-item-content>
                            <v-list-item-content>
                                <v-list-item-title v-text="item.timeStamp"></v-list-item-title>
                            </v-list-item-content>
                            <v-list-item-action>
                                <v-btn fab dark color="primary" @click="onEdit(item.id)"  >
                                    <v-icon>fas fa-edit</v-icon>
                                </v-btn>
                            </v-list-item-action>
                            <v-list-item-action>
                                <v-btn fab dark color="primary" @click="onView(item.id)"  >
                                    <v-icon>fas fa-eye</v-icon>
                                </v-btn>
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

        props: {
        },
        data: () => ({
        }),
        computed: {
            items() {
                return this.$store.getters["list/getItems"];
            }
        },
        mounted() {
            this.$store.dispatch("list/load");
        },
        methods: {
            onEdit(id) {
                if(id === undefined || id == -1) return;
                this.$router.push({ name: "testDetail", params: { id: id, isView: false } });
            },
            onView(id) {
                if(id === undefined || id == -1) return;
                this.$router.push({ name: "testDetail", params: { id: id, isView: true } });
            }
        }
    }
</script>

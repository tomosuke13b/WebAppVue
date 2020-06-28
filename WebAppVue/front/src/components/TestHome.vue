<template>
    <v-container>
        <v-row class="text-center">
            <v-col cols="12">
                <v-col class="mb-4">
                    <h1 class="display-2 font-weight-bold mb-3">
                        ホーム
                    </h1>
                </v-col>
                <v-col>
                    <v-btn small color="primary"  to="/test/list">List</v-btn>
                </v-col>
                <v-col>
                    <v-btn small color="primary"  to="/test/list/1">Detail</v-btn>
                </v-col>
                <v-col>
                    <v-btn small color="primary"  to="/test/new">New</v-btn>
                </v-col>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
    export default {
        name: 'TestHome',

        data: () => ({
        }),
        computed: {
        },
        mounted() {
        }
    }
</script>

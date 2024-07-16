<template>
    <div class="about">
        <h1>This is an about page:</h1>
        <v-btn @click="reqest">Button</v-btn>
    </div>
</template>

<script>
    import _ from "lodash";

    export default {
        props: {
            id: {
                type: Number,
                required: true
            }
        },

        data() {
            return {

            };
        },

        methods: {
            
        }
    };
</script>

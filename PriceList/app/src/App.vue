<template>
    <v-app>
        <v-navigation-drawer v-model="drawer"
                             app>
            <v-list dense
                    nav>
                <v-list-item link
                             color="primary"
                             :to="{ name: 'priceListsView' }"
                             exact>
                    <v-list-item-icon>
                        <v-icon>mdi-database-outline</v-icon>
                    </v-list-item-icon>

                    <v-list-item-content>
                        <v-list-item-title>Список прайс листов</v-list-item-title>
                    </v-list-item-content>
                </v-list-item>

                <v-list-item link
                             color="primary"
                             :to="{ name: 'addPriceListView' }"
                             exact>
                    <v-list-item-icon>
                        <v-icon>mdi-view-list-outline</v-icon>
                    </v-list-item-icon>

                    <v-list-item-content>
                        <v-list-item-title>Новый прайс лист</v-list-item-title>
                    </v-list-item-content>
                </v-list-item>
            </v-list>
        </v-navigation-drawer>

        <v-app-bar app
                   color="primary"
                   dark>
            <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
            <v-toolbar-title class="mr-10">База данных прайс-листов</v-toolbar-title>
            <v-btn @click="goBack" outlined >Назад</v-btn>
        </v-app-bar>

        <v-main>
            <v-container fluid>
                <router-view />
            </v-container>
        </v-main>

        <v-main>
            <v-snackbar :color="snackbarColor" :multi-line="snackbarText.length > 58" v-model="showSnackbar" top right>
                <div class="d-flex align-center">
                    <span>{{ snackbarText }}</span>
                    <v-spacer></v-spacer>
                    <v-btn class="pr-0"
                           plain
                           :ripple="false"
                           @click="showSnackbar = false">Закрыть</v-btn>
                </div>
            </v-snackbar>
        </v-main>
    </v-app>
</template>

<script>
    import { mapState } from "vuex";
    import PagesNavigation from "@/common/pagesNavigation";

    export default {
        name: "App",

        data() {
            return {
                drawer: null,
                showSnackbar: false,
                snackbarText: "",
                snackbarColor: "success"
            };
        },

        created() {
            this.$store.subscribe(mutation => {
                if (!this.showSnackbar && (mutation.type === "toast/success" || mutation.type === "toast/error" || mutation.type === "toast/warning")) {
                    this.showNotification(true);
                }
            });
        },

        computed: {
            ...mapState({
                notifications: state => state.toast.notifications
            })
        },

        watch: {
            showSnackbar(newValue) {
                if (newValue === false && this.notifications.length > 0) {
                    this.showNotification(false);
                }
            }
        },

        methods: {
            showNotification(immediately) {
                const notification = this.notifications[this.notifications.length - 1];
                this.$store.commit("toast/removeNotification", notification);

                this.snackbarText = notification.text;
                this.snackbarColor = notification.type;

                if (immediately) {
                    this.showSnackbar = true;
                } else {
                    this.$nextTick(() => this.showSnackbar = true);
                }
            },

            goBack: PagesNavigation.goBack
        }
    };
</script>
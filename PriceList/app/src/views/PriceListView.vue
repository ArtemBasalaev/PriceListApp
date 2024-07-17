<template>
    <div>
        <v-card flat class="ml-2">
            <v-card-title class="subheading font-weight-bold pl-0">{{ priceListName }} от {{ priceListDate }}</v-card-title>
        </v-card>

        <v-row>
            <v-col cols="12" sm="10" class="ml-2">
                <v-data-table :headers="headers"
                              :items="priceList"
                              :items-per-page="10"
                              class="elevation-1"
                              :search="search"
                              :custom-filter="searchPriceList">
                    <template v-slot:top>
                        <v-text-field v-model="search"
                                      label="Поиск по названию"
                                      class="mx-4"></v-text-field>
                    </template>

                    <template v-slot:item.name="{ item }">
                        <router-link :to="{ name: 'priceListView', params: { id: item.id } }">
                            <span>{{ item.name }}</span>
                        </router-link>
                    </template>
                </v-data-table>
            </v-col>
        </v-row>

        <v-row>
            <v-col>
                <v-btn depressed
                       class="ml-2"
                       color="primary"
                       @click="dialog = true">
                    Добавить товар
                </v-btn>
            </v-col>
        </v-row>

        <v-row justify="center">
            <v-dialog v-model="dialog"
                      persistent
                      max-width="600px">
                <v-card>
                    <v-card-title>
                        <span class="text-h5">Характеристики товара</span>
                    </v-card-title>

                    <v-card-text>
                        <v-container>
                            <validation-observer ref="observer" v-slot="{ handleSubmit }">
                                <form @submit.prevent="handleSubmit(savePricetList)">
                                    <v-row>
                                        <v-col cols="12">
                                            <validation-provider tag="div"
                                                                 v-slot="{ errors }"
                                                                 rules="required">
                                                <v-text-field v-model="product.name"
                                                              label="Название товара*"
                                                              :error-messages="errors">
                                                </v-text-field>
                                            </validation-provider>
                                        </v-col>

                                        <v-col cols="12">
                                            <validation-provider tag="div"
                                                                 v-slot="{ errors }"
                                                                 rules="required">
                                                <v-text-field v-model="product.code"
                                                              label="Код товара"
                                                              :error-messages="errors">
                                                </v-text-field>
                                            </validation-provider>
                                        </v-col>
                                    </v-row>

                                    <template v-for="(column, i) in columns">
                                        <v-row align="center" :key="i">
                                            <v-col cols="12" sm="2" class="py-0">
                                                <div class="text-body-1 font-weight-bold">{{ column.columnName.name }}</div>
                                            </v-col>

                                            <v-col cols="12" sm="10" class="py-0">
                                                <validation-provider tag="div"
                                                                     v-slot="{ errors }"
                                                                     rules="required">
                                                    <v-text-field v-if="column.columnType.id !== 2"
                                                                  v-model="productValue[column.columnName.id]"
                                                                  label="Значение"
                                                                  :placeholder="column.columnType.name"
                                                                  :error-messages="errors">
                                                    </v-text-field>
                                                    <v-textarea v-else
                                                                auto-grow
                                                                outlined
                                                                rows="3"
                                                                v-model="productValue[column.columnName.id]"
                                                                row-height="30"></v-textarea>
                                                </validation-provider>
                                            </v-col>
                                        </v-row>
                                    </template>
                                </form>
                            </validation-observer>
                        </v-container>
                        <small>* Поля необходимые для заполнения</small>
                    </v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-col>
                            <v-btn depressed
                                   class="mr-2 mb-2"
                                   color="error"
                                   @click="dialog = false">
                                Закрыть
                            </v-btn>

                            <v-btn depressed
                                   class="mb-2"
                                   color="success"
                                   @click="dialog = false"
                                   type="submit">
                                Сохранить
                            </v-btn>
                        </v-col>
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </v-row>
    </div>
</template>

<script>
    import _ from "lodash";
    import Utils from "@/common/utils";

    export default {
        props: {
            id: {
                type: Number,
                required: true
            }
        },

        data() {
            return {
                dialog: false,
                search: "",
                headers: [],
                priceList: [],
                priceListName: "",
                priceListDate: null,
                columns: [],
                product: {
                    name: "",
                    code: "",
                },
                productValue: {}
            }
        },

        created() {
            this.loadData();
        },

        methods: {
            loadData() {
                this.$store.dispatch("getPriceList", this.id).then(({ columns, priceList, productsPriceListData }) => {
                    this.columns = columns
                    this.priceListName = priceList.name;
                    this.priceListDate = this.formatDate(priceList.creationDate);

                    const headers = columns.map(c => {
                        return {
                            text: c.columnName.name,
                            align: "start",
                            value: c.columnName.id,
                        }
                    });

                    this.headers = [
                        {
                            text: "Номер",
                            value: "number",
                        },
                        {
                            text: "Название",
                            align: "start",
                            value: "productName",
                        },
                        {
                            text: "Код",
                            align: "start",
                            value: "productCode",
                        },
                        ...headers
                    ];
                })
            },

            formatDate: Utils.formatDate,

            searchPriceList(value, search, _) {
                return value != null &&
                    search != null &&
                    value.toString().trim().toLocaleLowerCase().indexOf(search.trim().toLocaleLowerCase()) !== -1;
            }
        }
    };
</script>

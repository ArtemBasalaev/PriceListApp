<template>
    <div>
        <v-card flat class="ml-2">
            <v-card-title class="subheading font-weight-bold pl-0">{{ priceListName }} от {{ priceListDate }}</v-card-title>
        </v-card>

        <v-row>
            <v-col cols="12" sm="10" class="ml-2">
                <v-data-table :headers="headers"
                              :items="priceListData"
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
                                <form @submit.prevent="handleSubmit(saveNewProduct)">
                                    <v-row>
                                        <v-col cols="12">
                                            <validation-provider tag="div"
                                                                 v-slot="{ errors }"
                                                                 rules="required">
                                                <v-text-field v-model="newProduct.name"
                                                              label="Название товара*"
                                                              :error-messages="errors">
                                                </v-text-field>
                                            </validation-provider>
                                        </v-col>

                                        <v-col cols="12">
                                            <validation-provider tag="div"
                                                                 v-slot="{ errors }"
                                                                 rules="required">
                                                <v-text-field v-model="newProduct.code"
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
                                                    <v-text-field v-if="column.columnType.id === 1"
                                                                  v-model="newProduct.data[column.priceListColumnId]"
                                                                  label="Значение"
                                                                  :placeholder="column.columnType.name"
                                                                  :error-messages="errors">
                                                    </v-text-field>
                                                    <v-textarea v-else-if="2"
                                                                auto-grow
                                                                outlined
                                                                rows="3"
                                                                v-model="newProduct.data[column.priceListColumnId]"
                                                                row-height="30"></v-textarea>
                                                    <v-text-field v-else
                                                                  v-model="newProduct.data[column.priceListColumnId]"
                                                                  label="Значение"
                                                                  type="number"
                                                                  :placeholder="column.columnType.name"
                                                                  :error-messages="errors">
                                                    </v-text-field>
                                                </validation-provider>
                                            </v-col>
                                        </v-row>
                                    </template>

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
                                </form>
                            </validation-observer>
                        </v-container>
                        <small>* Поля необходимые для заполнения</small>
                    </v-card-text>
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
                required: true
            }
        },

        data() {
            return {
                dialog: false,
                search: "",
                headers: [],
                priceListData: [],
                priceListName: "",
                priceListDate: null,
                columns: [],
                newProduct: {
                    name: "",
                    code: "",
                    data: {}
                }
            }
        },

        created() {
            this.loadData();
        },

        methods: {
            loadData() {
                this.$store.dispatch("getPriceList", this.id).then(({ columns, priceList, productsPriceListData }) => {
                    this.columns = columns;
                    this.priceListName = priceList.name;
                    this.priceListDate = this.formatDate(priceList.creationDate);

                    const headers = columns.map(c => {
                        return {
                            text: c.columnName.name,
                            align: "start",
                            value: c.priceListColumnId,
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

                    if (productsPriceListData && productsPriceListData.length != 0) {
                        this.priceListData = productsPriceListData.map((p, index) => {
                            const product = {
                                number: index + 1,
                                name: p.name,
                                code: p.code
                            }

                            const columnsData = {};

                            for (const columnData of p.columnsData) {
                                columnsData[columnData.id] = columnData.value;
                            }

                            return { ...product, ...columnsData }
                        })
                    }
                })
            },

            formatDate: Utils.formatDate,

            searchPriceList(value, search, _) {
                return value != null &&
                    search != null &&
                    value.toString().trim().toLocaleLowerCase().indexOf(search.trim().toLocaleLowerCase()) !== -1;
            },

            saveNewProduct() {
                const newProductRequest = {
                    name: this.newProduct.name,
                    code: this.newProduct.code
                };

                this.$store.dispatch("addNewProduct", newProductRequest).then(product => {
                    const columnsData = [];

                    for (const [priceListColumnId, value] of Object.entries(this.newProduct.data)) {
                        columnsData.push({
                            priceListColumnId,
                            value
                        });
                    }

                    const addProductDataToPriceListRequest = {
                        productId: product.id,
                        columnsData
                    };

                    this.$store.dispatch("addDataInPriceList", addProductDataToPriceListRequest).then(_ => {
                        this.priceListName = "";
                        this.priceListColumns = [];
                        this.$refs.observer.reset();
                    })
                })
            }
        }
    };
</script>

// 共用元件註冊器
class ComponentManager {
    constructor() {
        this.componentPromises = [];
    }

    // 註冊元件的通用方法
    registerComponent(name, url, options = {}) {
        const promise = fetch(url)
            .then(res => res.text())
            .then(template => {
                Vue.component(name, {
                    name: options.name || name,
                    template: template,
                    ...(options.definition || {})
                });
            });
        this.componentPromises.push(promise);
    }

    // 註冊所有共用元件
    registerAllComponents() {
        // 標題元件
        this.registerComponent("header-bar", "../assets/components/header-bar.html", {
            definition: {
                props: ["categories", "reset", "show-search"],
                data() {
                    return {
                        searchCategory: "所有書籍"
                    }
                },
                watch: {
                    reset(newVal) {
                        if (newVal) {
                            this.searchCategory = "所有書籍";
                        }
                    }
                }
            }
        });

        // 登入 / 註冊元件
        this.registerComponent("login-modal", "../assets/components/login-modal.html", {
            definition: {
                props: ["visible"],
                data() {
                    return {
                        username: "",
                        password: "",
                        email: "",
                        confirmPassword: "",
                        isRegistering: false
                    };
                },
                watch: {
                    visible(newVal) {
                        if (newVal) {
                            this.isRegistering = false;
                            this.resetForm();
                        }
                    }
                },
                methods: {
                    login() {
                        this.resetForm();
                        if (this.email && this.password) {
                            alert(`登入成功\n帳號：${this.username}`);
                            this.$emit('close');
                        } else {
                            alert("請輸入帳號與密碼");
                        }
                    },
                    register() {
                        this.resetForm();
                        this.isRegistering = true;
                    },
                    cancel() {
                        this.resetForm();
                        this.isRegistering = false;
                    },
                    resetForm() {
                        this.username = "";
                        this.password = "";
                        this.email = "";
                        this.confirmPassword = "";
                    }
                }
            }
        });

        // 側邊欄元件
        this.registerComponent("side-bar", "../assets/components/side-bar.html", {
            definition: {
                props: ["categories", "currentcategory"],
            }
        });

        // 輪播元件
        this.registerComponent("books-carousel", "../assets/components/books-carousel.html", {
            definition: {
                props: ["allCategories", "books"],
                methods: {
                    emitSelectCategory(category) {
                        this.$emit("select-category", category);
                    },
                    booksByCategory(categoryValue) {
                        return this.books.filter(book => book.category === categoryValue);
                    },
                    showBookDetail(book) {
                        this.$emit("select-book", book);
                    },
                    initSwipers() {
                        this.allCategories.forEach((cat, index) => {
                            const refItem = this.$refs['swiper' + index];
                            const container = Array.isArray(refItem) ? refItem[0] : refItem;

                            if (container instanceof Element) {
                                new Swiper(container, {
                                    loop: true,
                                    slidesPerView: 4,
                                    spaceBetween: 20,
                                    pagination: {
                                        el: ".swiper-pagination",
                                        clickable: true,
                                        dynamicBullets: true,
                                    },
                                    navigation: {
                                        nextEl: ".swiper-button-next",
                                        prevEl: ".swiper-button-prev",
                                    },
                                    breakpoints: {
                                        0: { slidesPerView: 1 },
                                        768: { slidesPerView: 3 },
                                        1024: { slidesPerView: 5 }
                                    }
                                });
                            } else {
                                console.warn("swiper 容器無效", cat.value, refItem);
                            }
                        });
                    },
                    
                    // todo
                    async fetchCategories() {
                        const res = await axios.get("/api/home/categories");
                        this.categories = res.data;

                        // 為每個分類載入對應的書籍
                        for (const cat of this.categories) {
                            this.fetchBooksForCategory(cat.name);
                        }
                    },

                    async fetchBooksForCategory(categoryName) {
                        const res = await axios.get(`/api/home/carousel/${encodeURIComponent(categoryName)}`);
                        this.$set(this.booksByCategory, categoryName, res.data);
                    }
                },
                mounted() {                    
                    this.$nextTick(this.initSwipers);
                    // todo
                    this.fetchCategories();
                }
            }
        });

        // 書籍分類元件
        this.registerComponent("books-list", "../assets/components/books-list.html", {
            definition: {
                props: ["books", "title"],
                methods: {
                    showBookDetail(book) {
                        this.$emit("select-book", book);
                    },
                }
            }
        });

        // 書籍內容元件
        this.registerComponent("book-detail", "../assets/components/book-detail.html", {
            definition: {
                props: ["book"],
            }
        });

        // 會員元件
        this.registerComponent("member-page", "../assets/components/member-page.html", {
            definition: {
                props: ["memberfields", "editstatus", "member", "reset"],
                data() {
                    return {
                        searchCategory: "所有書籍"
                    }
                },
                watch: {
                    reset(newVal) {
                        if (newVal) {
                            this.searchCategory = "所有書籍";
                        }
                    }
                }
            }
        });

        // 購物車元件
        this.registerComponent("cart-page", "../assets/components/cart-page.html", {
            definition: {
                props:["cartitems"],
                
            }
        })

        // 訂單元件
        this.registerComponent("orders-page", "../assets/components/orders-page.html", {
            definition: {
                props:["paginatedorders"],
                data(){
                    return{
                        currentOrderTab: "orders"
                    }                
                }
            }
        })

        // 訂單詳細資料元件
        // 訂單元件
        this.registerComponent("order-detail-page", "../assets/components/order-detail-page.html", {
            definition: {
                props:["paginatedorders"],
                data(){
                    return{
                        currentOrderTab: "orders",
                        currentPage: 1,
                        totalPages: 2
                    }                
                }
            }
        })

        // 頁尾元件
        this.registerComponent("footer-bar", "../assets/components/footer-bar.html", {
            definition: {
                props: ["categories"],
            }
        });
    }

    // 等待所有元件載入完成
    waitForAllComponents() {
        return Promise.all(this.componentPromises);
    }
}

// 共用的 Vue 實例 mixin
const CommonVueMixin = {
    data() {
        return {
            showLogin: false,
            currentTab: null,
            selectedBook: null,
            resetSearch: null,
        }
    },
    computed: {
        specialCategories() {
            return this.categories.filter(c => !c.isSpecial)
        },
        allCategories() {
            return this.categories.filter(c => !c.isAllCategory)
        },
        filteredBooks() {
            if (!this.currentTab) return []
            if (this.currentTab === "所有書籍") {
                return this.books || [];
            }
            return (this.books || []).filter(book => book.category === this.currentTab)
        }
    },
    methods: {
        selectCategory(categoryValue) {
            this.currentTab = categoryValue;
            this.selectedBook = null;
            window.scrollTo(0, 0);
        },
        
        showLoginModal() {
            this.showLogin = true;
        },

        closeLoginModal() {
            this.showLogin = false;
        },

        goHome() {
            this.currentTab = null;
            this.selectedBook = null;
            this.resetSearch = true;

            this.$nextTick(() => {
                this.resetSearch = false;
            });
        },
    }
};

// 初始化應用程式的通用函數
function initializeApp(specificData = {}, specificMethods = {}) {
    const componentManager = new ComponentManager();
    componentManager.registerAllComponents();
    
    return componentManager.waitForAllComponents().then(() => {
        return new Vue({
            el: "#app",
            mixins: [CommonVueMixin],
            data: {
                ...CommonVueMixin.data(),
                ...specificData
            },
            methods: {
                ...CommonVueMixin.methods,
                ...specificMethods
            },
            mounted() {
                // 可以在這裡添加頁面特定的初始化邏輯
            }
        });
    });
}
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
                props: ["headerCategories", "showSearch"],
                data() {
                    return {
                        searchCategory: "",
                    }
                },                
                watch: {
                    headerCategories(newVal) {
                        if (newVal && newVal.length > 0) {
                            this.searchCategory = newVal[0].name;
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
                props: ["sidebarCategories", "currentCategory"],
            }
        });

        // 輪播元件
        this.registerComponent("books-carousel", "../assets/components/books-carousel.html", {
            definition: {
                props: ["allCategories", "carouselBooksMap"],
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
                        // console.log('initswipers - start');
                        this.allCategories.forEach((cat, index) => {
                            let refItem = this.$refs['swiper' + index];
                            let container = Array.isArray(refItem) ? refItem[0] : refItem;
                            
                            // 判斷是否已初始化過
                            if (!container || container.swiper) {
                                return;
                            }

                            if (container instanceof Element) {
                                let paginationEl = container.querySelector(".swiper-pagination");
                                let nextEl = container.querySelector(".swiper-button-next");
                                let prevEl = container.querySelector(".swiper-button-prev");
                                let bookCount = this.carouselBooksMap[cat.name]?.length || 0;
                                new Swiper(container, {
                                    loop: bookCount > 4,
                                    slidesPerView: Math.min(bookCount, 4),
                                    spaceBetween: 20,
                                    pagination: {
                                        el: paginationEl,
                                        clickable: true,
                                        dynamicBullets: true,
                                    },
                                    navigation: {
                                        nextEl: nextEl,
                                        prevEl: prevEl,
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
                        //  console.log('initswipers - end');
                    },
                                    
                },
                watch: {
                    carouselBooksMap: {
                        handler() {
                            this.$nextTick(() => {
                                setTimeout(() => {
                                    this.initSwipers();
                                }, 50);
                            });
                        },
                        deep: true,
                    }
                },
                mounted() {
                    this.initSwipers();
                    // console.log("初始化 Swiper");
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
                        searchCategory: ""
                    }
                },
                watch: {
                    reset(newVal) {
                        if (newVal) {
                            this.searchCategory = "所有作品";
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
            // 預設狀態資料，控制登入視窗、目前分類、目前點擊的書籍、是否要重置搜尋分類
            showLogin: false,
            currentTab: null,
            selectedBook: null,
            resetSearch: null,
        }
    },
    computed: {       
        filteredBooks() {
            if (!this.currentTab) return []
            if (this.currentTab === "所有作品") {
                return this.books || [];
            }
            return (this.books || []).filter(book => book.category === this.currentTab)
        }
    },
    methods: {
        // 預設分類
        getDefaultCategory() {
            const allCat = this.categories.find(c => c.categoryID === -3);
            return allCat ? allCat.name : "";
        },

        // 更新目前的分類
        selectCategory(categoryValue) {
            this.currentTab = categoryValue;
            this.selectedBook = null;
            window.scrollTo(0, 0);
        },
        
        //  控制登入/註冊視窗顯示與隱藏
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
function initializeApp(specificData = {}, specificMethods = {}, extraOptions = {}) {
    // 載入 HTML 元件（ 例如 header-bar、login-modal ...）
    const componentManager = new ComponentManager();
    // 一次註冊所有共用 Vue 組件
    componentManager.registerAllComponents();
    
    //  等待所有組件（使用 fetch(url) 載入）都完成註冊後再執行 Vue 實例初始化
    return componentManager.waitForAllComponents().then(() => {
        return new Vue({
            el: "#app",
            // 使用一個共用 mixin，提供共享的 data、methods、computed 屬性
            mixins: [CommonVueMixin],
            data: {
                // 合併共用資料 + 專屬頁面資料 (categories, books)
                ...CommonVueMixin.data(),
                // 合併共用方法（例如 goHome, selectCategory）和頁面專屬方法（fetchCategories）
                ...specificData
            },
            methods: {
                ...CommonVueMixin.methods,
                ...specificMethods
            },
            mounted() {
                console.log('app - start');
            },
            ...extraOptions
        });
    });
}
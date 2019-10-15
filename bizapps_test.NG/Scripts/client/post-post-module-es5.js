function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["post-post-module"], {
  /***/
  "./node_modules/raw-loader/dist/cjs.js!./src/app/post/post.component.html":
  /*!********************************************************************************!*\
    !*** ./node_modules/raw-loader/dist/cjs.js!./src/app/post/post.component.html ***!
    \********************************************************************************/

  /*! exports provided: default */

  /***/
  function node_modulesRawLoaderDistCjsJsSrcAppPostPostComponentHtml(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony default export */


    __webpack_exports__["default"] = "<div class=\"row\">\r\n  <div id=\"postView\" class=\" col-lg-9 PostViewContainer\">\r\n    <input id=\"postId\" [value]=\"post.Id\" type=\"hidden\" />\r\n    <img src=\"/Images/Default.jpeg\" class=\"postimage\">\r\n      <h2 id=\"PostTitle\">{{post.PostTitle}}</h2>\r\n      <label id=\"labelDate\" class=\"text-right\"></label>\r\n    <div class=\"divBodyHolder\" id=\"divBodyHolder\" >{{post.PostContent}}</div>\r\n      Categories:\r\n      <ul *ngFor=\"let category of post.PostCategories\" class=\"list-inline\" style=\"font-style: italic;\">\r\n        <li>\r\n          <span>{{category.CategoryName}}</span>\r\n        </li>\r\n      </ul>\r\n    <br/>\r\n  </div>\r\n</div>\r\n";
    /***/
  },

  /***/
  "./src/app/Adapters/PostAdapter.ts":
  /*!*****************************************!*\
    !*** ./src/app/Adapters/PostAdapter.ts ***!
    \*****************************************/

  /*! exports provided: PostAdapter */

  /***/
  function srcAppAdaptersPostAdapterTs(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony export (binding) */


    __webpack_require__.d(__webpack_exports__, "PostAdapter", function () {
      return PostAdapter;
    });
    /* harmony import */


    var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
    /*! tslib */
    "./node_modules/tslib/tslib.es6.js");
    /* harmony import */


    var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
    /*! @angular/core */
    "./node_modules/@angular/core/fesm2015/core.js");
    /* harmony import */


    var src_app_Models_Post__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
    /*! src/app/Models/Post */
    "./src/app/Models/Post.ts");

    var PostAdapter =
    /*#__PURE__*/
    function () {
      function PostAdapter() {
        _classCallCheck(this, PostAdapter);
      }

      _createClass(PostAdapter, [{
        key: "adapt",
        value: function adapt(item) {
          if (item == null) return null;
          var post = new src_app_Models_Post__WEBPACK_IMPORTED_MODULE_2__["Post"]();
          post.Id = item.Id;
          post.PostContent = item.PostContent;
          post.PostTitle = item.PostTitle;
          post.PostCategories = item.PostCategories;
          post.CreationDate = new Date(item.CreationDate);
          return post;
        }
      }]);

      return PostAdapter;
    }();

    PostAdapter = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])({
      providedIn: 'root'
    })], PostAdapter);
    /***/
  },

  /***/
  "./src/app/Models/Post.ts":
  /*!********************************!*\
    !*** ./src/app/Models/Post.ts ***!
    \********************************/

  /*! exports provided: Post */

  /***/
  function srcAppModelsPostTs(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony export (binding) */


    __webpack_require__.d(__webpack_exports__, "Post", function () {
      return Post;
    });
    /* harmony import */


    var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
    /*! tslib */
    "./node_modules/tslib/tslib.es6.js");

    var Post = function Post() {
      _classCallCheck(this, Post);

      this.PostCategories = new Array();
    };
    /***/

  },

  /***/
  "./src/app/post/post-routing.module.ts":
  /*!*********************************************!*\
    !*** ./src/app/post/post-routing.module.ts ***!
    \*********************************************/

  /*! exports provided: PostRoutingModule */

  /***/
  function srcAppPostPostRoutingModuleTs(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony export (binding) */


    __webpack_require__.d(__webpack_exports__, "PostRoutingModule", function () {
      return PostRoutingModule;
    });
    /* harmony import */


    var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
    /*! tslib */
    "./node_modules/tslib/tslib.es6.js");
    /* harmony import */


    var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
    /*! @angular/core */
    "./node_modules/@angular/core/fesm2015/core.js");
    /* harmony import */


    var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
    /*! @angular/router */
    "./node_modules/@angular/router/fesm2015/router.js");
    /* harmony import */


    var _post_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
    /*! ./post.component */
    "./src/app/post/post.component.ts");

    var routes = [{
      path: 'post/:id',
      redirectTo: '/blogpost/:id'
    }, {
      path: 'blogpost/:id',
      component: _post_component__WEBPACK_IMPORTED_MODULE_3__["PostComponent"]
    }];

    var PostRoutingModule = function PostRoutingModule() {
      _classCallCheck(this, PostRoutingModule);
    };

    PostRoutingModule = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["NgModule"])({
      imports: [_angular_router__WEBPACK_IMPORTED_MODULE_2__["RouterModule"].forChild(routes)],
      exports: [_angular_router__WEBPACK_IMPORTED_MODULE_2__["RouterModule"]]
    })], PostRoutingModule);
    /***/
  },

  /***/
  "./src/app/post/post.component.css":
  /*!*****************************************!*\
    !*** ./src/app/post/post.component.css ***!
    \*****************************************/

  /*! exports provided: default */

  /***/
  function srcAppPostPostComponentCss(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony default export */


    __webpack_exports__["default"] = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3Bvc3QvcG9zdC5jb21wb25lbnQuY3NzIn0= */";
    /***/
  },

  /***/
  "./src/app/post/post.component.ts":
  /*!****************************************!*\
    !*** ./src/app/post/post.component.ts ***!
    \****************************************/

  /*! exports provided: PostComponent */

  /***/
  function srcAppPostPostComponentTs(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony export (binding) */


    __webpack_require__.d(__webpack_exports__, "PostComponent", function () {
      return PostComponent;
    });
    /* harmony import */


    var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
    /*! tslib */
    "./node_modules/tslib/tslib.es6.js");
    /* harmony import */


    var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
    /*! @angular/core */
    "./node_modules/@angular/core/fesm2015/core.js");
    /* harmony import */


    var _post_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
    /*! ./post.service */
    "./src/app/post/post.service.ts");
    /* harmony import */


    var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
    /*! @angular/router */
    "./node_modules/@angular/router/fesm2015/router.js");
    /* harmony import */


    var src_app_Models_Post__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
    /*! src/app/Models/Post */
    "./src/app/Models/Post.ts");

    var PostComponent =
    /*#__PURE__*/
    function () {
      function PostComponent(postService, route, router) {
        var _this = this;

        _classCallCheck(this, PostComponent);

        this.postService = postService;
        this.route = route;
        this.router = router;
        this.post = new src_app_Models_Post__WEBPACK_IMPORTED_MODULE_4__["Post"]();
        this.route.params.subscribe(function (param) {
          _this.postId = param['id'];
        });
      }

      _createClass(PostComponent, [{
        key: "ngOnInit",
        value: function ngOnInit() {
          var _this2 = this;

          this.postService.GetPostByID(this.postId).subscribe(function (data) {
            _this2.post = Object.assign({}, data);

            if (_this2.post.Id == undefined || 0) {
              _this2.router.navigate(['/404']);
            }
          });
        }
      }]);

      return PostComponent;
    }();

    PostComponent.ctorParameters = function () {
      return [{
        type: _post_service__WEBPACK_IMPORTED_MODULE_2__["PostService"]
      }, {
        type: _angular_router__WEBPACK_IMPORTED_MODULE_3__["ActivatedRoute"]
      }, {
        type: _angular_router__WEBPACK_IMPORTED_MODULE_3__["Router"]
      }];
    };

    PostComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
      selector: 'app-post',
      template: tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(
      /*! raw-loader!./post.component.html */
      "./node_modules/raw-loader/dist/cjs.js!./src/app/post/post.component.html")).default,
      styles: [tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(
      /*! ./post.component.css */
      "./src/app/post/post.component.css")).default]
    })], PostComponent);
    /***/
  },

  /***/
  "./src/app/post/post.module.ts":
  /*!*************************************!*\
    !*** ./src/app/post/post.module.ts ***!
    \*************************************/

  /*! exports provided: PostModule */

  /***/
  function srcAppPostPostModuleTs(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony export (binding) */


    __webpack_require__.d(__webpack_exports__, "PostModule", function () {
      return PostModule;
    });
    /* harmony import */


    var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
    /*! tslib */
    "./node_modules/tslib/tslib.es6.js");
    /* harmony import */


    var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
    /*! @angular/core */
    "./node_modules/@angular/core/fesm2015/core.js");
    /* harmony import */


    var _angular_common__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
    /*! @angular/common */
    "./node_modules/@angular/common/fesm2015/common.js");
    /* harmony import */


    var _post_routing_module__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
    /*! ./post-routing.module */
    "./src/app/post/post-routing.module.ts");
    /* harmony import */


    var _post_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
    /*! ./post.component */
    "./src/app/post/post.component.ts");
    /* harmony import */


    var _post_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
    /*! ./post.service */
    "./src/app/post/post.service.ts");

    var PostModule = function PostModule() {
      _classCallCheck(this, PostModule);
    };

    PostModule = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["NgModule"])({
      declarations: [_post_component__WEBPACK_IMPORTED_MODULE_4__["PostComponent"]],
      imports: [_angular_common__WEBPACK_IMPORTED_MODULE_2__["CommonModule"], _post_routing_module__WEBPACK_IMPORTED_MODULE_3__["PostRoutingModule"]],
      providers: [_post_service__WEBPACK_IMPORTED_MODULE_5__["PostService"]]
    })], PostModule);
    /***/
  },

  /***/
  "./src/app/post/post.service.ts":
  /*!**************************************!*\
    !*** ./src/app/post/post.service.ts ***!
    \**************************************/

  /*! exports provided: PostService */

  /***/
  function srcAppPostPostServiceTs(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony export (binding) */


    __webpack_require__.d(__webpack_exports__, "PostService", function () {
      return PostService;
    });
    /* harmony import */


    var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
    /*! tslib */
    "./node_modules/tslib/tslib.es6.js");
    /* harmony import */


    var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
    /*! @angular/core */
    "./node_modules/@angular/core/fesm2015/core.js");
    /* harmony import */


    var _angular_common_http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
    /*! @angular/common/http */
    "./node_modules/@angular/common/fesm2015/http.js");
    /* harmony import */


    var src_app_config_config_json__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
    /*! src/app/config/config.json */
    "./src/app/config/config.json");

    var src_app_config_config_json__WEBPACK_IMPORTED_MODULE_3___namespace =
    /*#__PURE__*/
    __webpack_require__.t(
    /*! src/app/config/config.json */
    "./src/app/config/config.json", 1);
    /* harmony import */


    var rxjs_operators__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
    /*! rxjs/operators */
    "./node_modules/rxjs/_esm2015/operators/index.js");
    /* harmony import */


    var _Adapters_PostAdapter__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
    /*! ../Adapters/PostAdapter */
    "./src/app/Adapters/PostAdapter.ts");

    var PostService =
    /*#__PURE__*/
    function () {
      function PostService(http, adapter) {
        _classCallCheck(this, PostService);

        this.http = http;
        this.adapter = adapter;
      }

      _createClass(PostService, [{
        key: "GetPostByID",
        value: function GetPostByID(postID) {
          var _this3 = this;

          var url = src_app_config_config_json__WEBPACK_IMPORTED_MODULE_3__.apiUrl + src_app_config_config_json__WEBPACK_IMPORTED_MODULE_3__.getPostByIdUrl + postID;
          var post = this.http.get(url).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (data) {
            return _this3.adapter.adapt(data);
          }));
          return post;
        }
      }]);

      return PostService;
    }();

    PostService.ctorParameters = function () {
      return [{
        type: _angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClient"]
      }, {
        type: _Adapters_PostAdapter__WEBPACK_IMPORTED_MODULE_5__["PostAdapter"]
      }];
    };

    PostService = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])({
      providedIn: 'root'
    })], PostService);
    /***/
  }
}]); //# sourceMappingURL=post-post-module-es2015.js.map
//# sourceMappingURL=post-post-module-es5.js.map
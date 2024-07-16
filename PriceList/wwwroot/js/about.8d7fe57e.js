"use strict";(self["webpackChunkfrontend"]=self["webpackChunkfrontend"]||[]).push([[594],{359:function(t,e,i){i.r(e),i.d(e,{default:function(){return Z}});var n=i(6848);function r(){for(var t=arguments.length,e=Array(t),i=0;i<t;i++)e[i]=arguments[i];return n.Ay.extend({mixins:e})}i(4114);var s=function(){function t(t,e){var i=[],n=!0,r=!1,s=void 0;try{for(var a,o=t[Symbol.iterator]();!(n=(a=o.next()).done);n=!0)if(i.push(a.value),e&&i.length===e)break}catch(l){r=!0,s=l}finally{try{!n&&o["return"]&&o["return"]()}finally{if(r)throw s}}return i}return function(e,i){if(Array.isArray(e))return e;if(Symbol.iterator in Object(e))return t(e,i);throw new TypeError("Invalid attempt to destructure non-iterable instance")}}(),a=Object.assign||function(t){for(var e=1;e<arguments.length;e++){var i=arguments[e];for(var n in i)Object.prototype.hasOwnProperty.call(i,n)&&(t[n]=i[n])}return t};function o(t,e,i){return e in t?Object.defineProperty(t,e,{value:i,enumerable:!0,configurable:!0,writable:!0}):t[e]=i,t}function l(t){return!!t&&!!t.match(/^(#|(rgb|hsl)a?\()/)}var c=n.Ay.extend({name:"colorable",props:{color:String},methods:{setBackgroundColor:function(t){var e=arguments.length>1&&void 0!==arguments[1]?arguments[1]:{};return l(t)?e.style=a({},e.style,{"background-color":""+t,"border-color":""+t}):t&&(e.class=a({},e.class,o({},t,!0))),e},setTextColor:function(t){var e=arguments.length>1&&void 0!==arguments[1]?arguments[1]:{};if(l(t))e.style=a({},e.style,{color:""+t,"caret-color":""+t});else if(t){var i=t.toString().trim().split(" ",2),n=s(i,2),r=n[0],c=n[1];e.class=a({},e.class,o({},r+"--text",!0)),c&&(e.class["text--"+c]=!0)}return e}}}),u=r(c).extend({name:"v-progress-circular",props:{button:Boolean,indeterminate:Boolean,rotate:{type:[Number,String],default:0},size:{type:[Number,String],default:32},width:{type:[Number,String],default:4},value:{type:[Number,String],default:0}},computed:{calculatedSize:function(){return Number(this.size)+(this.button?8:0)},circumference:function(){return 2*Math.PI*this.radius},classes:function(){return{"v-progress-circular--indeterminate":this.indeterminate,"v-progress-circular--button":this.button}},normalizedValue:function(){return this.value<0?0:this.value>100?100:parseFloat(this.value)},radius:function(){return 20},strokeDashArray:function(){return Math.round(1e3*this.circumference)/1e3},strokeDashOffset:function(){return(100-this.normalizedValue)/100*this.circumference+"px"},strokeWidth:function(){return Number(this.width)/+this.size*this.viewBoxSize*2},styles:function(){return{height:this.calculatedSize+"px",width:this.calculatedSize+"px"}},svgStyles:function(){return{transform:"rotate("+Number(this.rotate)+"deg)"}},viewBoxSize:function(){return this.radius/(1-Number(this.width)/+this.size)}},methods:{genCircle:function(t,e,i){return t("circle",{class:"v-progress-circular__"+e,attrs:{fill:"transparent",cx:2*this.viewBoxSize,cy:2*this.viewBoxSize,r:this.radius,"stroke-width":this.strokeWidth,"stroke-dasharray":this.strokeDashArray,"stroke-dashoffset":i}})},genSvg:function(t){var e=[this.indeterminate||this.genCircle(t,"underlay",0),this.genCircle(t,"overlay",this.strokeDashOffset)];return t("svg",{style:this.svgStyles,attrs:{xmlns:"http://www.w3.org/2000/svg",viewBox:this.viewBoxSize+" "+this.viewBoxSize+" "+2*this.viewBoxSize+" "+2*this.viewBoxSize}},e)}},render:function(t){var e=t("div",{staticClass:"v-progress-circular__info"},this.$slots.default),i=this.genSvg(t);return t("div",this.setTextColor(this.color,{staticClass:"v-progress-circular",attrs:{role:"progressbar","aria-valuemin":0,"aria-valuemax":100,"aria-valuenow":this.indeterminate?void 0:this.normalizedValue},class:this.classes,style:this.styles,on:this.$listeners}),[i,e])}}),h=u,d=i(5604);function p(t,e,i){return e in t?Object.defineProperty(t,e,{value:i,enumerable:!0,configurable:!0,writable:!0}):t[e]=i,t}function v(t,e){return function(){return(0,d.OP)("The "+t+" component must be used inside a "+e)}}function f(t,e,i){var r=e&&i?{register:v(e,i),unregister:v(e,i)}:null;return n.Ay.extend({name:"registrable-inject",inject:p({},t,{default:r})})}function m(t,e,i){return e in t?Object.defineProperty(t,e,{value:i,enumerable:!0,configurable:!0,writable:!0}):t[e]=i,t}function b(t,e,i){return f(t,e,i).extend({name:"groupable",props:{activeClass:{type:String,default:function(){if(this[t])return this[t].activeClass}},disabled:Boolean},data:function(){return{isActive:!1}},computed:{groupClasses:function(){return this.activeClass?m({},this.activeClass,this.isActive):{}}},created:function(){this[t]&&this[t].register(this)},beforeDestroy:function(){this[t]&&this[t].unregister(this)},methods:{toggle:function(){this.$emit("change")}}})}b("itemGroup");var g=i(6960),y={absolute:Boolean,bottom:Boolean,fixed:Boolean,left:Boolean,right:Boolean,top:Boolean};function x(){var t=arguments.length>0&&void 0!==arguments[0]?arguments[0]:[];return n.Ay.extend({name:"positionable",props:t.length?(0,g.fF)(y,t):y})}var k=x();function w(t,e){t.style["transform"]=e,t.style["webkitTransform"]=e}function _(t,e){t.style["opacity"]=e.toString()}function C(t){return"TouchEvent"===t.constructor.name}var B=function(t,e){var i=arguments.length>2&&void 0!==arguments[2]?arguments[2]:{},n=e.getBoundingClientRect(),r=C(t)?t.touches[t.touches.length-1]:t,s=r.clientX-n.left,a=r.clientY-n.top,o=0,l=.3;e._ripple&&e._ripple.circle?(l=.15,o=e.clientWidth/2,o=i.center?o:o+Math.sqrt(Math.pow(s-o,2)+Math.pow(a-o,2))/4):o=Math.sqrt(Math.pow(e.clientWidth,2)+Math.pow(e.clientHeight,2))/2;var c=(e.clientWidth-2*o)/2+"px",u=(e.clientHeight-2*o)/2+"px",h=i.center?c:s-o+"px",d=i.center?u:a-o+"px";return{radius:o,scale:l,x:h,y:d,centerX:c,centerY:u}},S={show:function(t,e){var i=arguments.length>2&&void 0!==arguments[2]?arguments[2]:{};if(e._ripple&&e._ripple.enabled){var n=document.createElement("span"),r=document.createElement("span");n.appendChild(r),n.className="v-ripple__container",i.class&&(n.className+=" "+i.class);var s=B(t,e,i),a=s.radius,o=s.scale,l=s.x,c=s.y,u=s.centerX,h=s.centerY,d=2*a+"px";r.className="v-ripple__animation",r.style.width=d,r.style.height=d,e.appendChild(n);var p=window.getComputedStyle(e);p&&"static"===p.position&&(e.style.position="relative",e.dataset.previousPosition="static"),r.classList.add("v-ripple__animation--enter"),r.classList.add("v-ripple__animation--visible"),w(r,"translate("+l+", "+c+") scale3d("+o+","+o+","+o+")"),_(r,0),r.dataset.activated=String(performance.now()),setTimeout((function(){r.classList.remove("v-ripple__animation--enter"),r.classList.add("v-ripple__animation--in"),w(r,"translate("+u+", "+h+") scale3d(1,1,1)"),_(r,.25)}),0)}},hide:function(t){if(t&&t._ripple&&t._ripple.enabled){var e=t.getElementsByClassName("v-ripple__animation");if(0!==e.length){var i=e[e.length-1];if(!i.dataset.isHiding){i.dataset.isHiding="true";var n=performance.now()-Number(i.dataset.activated),r=Math.max(250-n,0);setTimeout((function(){i.classList.remove("v-ripple__animation--in"),i.classList.add("v-ripple__animation--out"),_(i,0),setTimeout((function(){var e=t.getElementsByClassName("v-ripple__animation");1===e.length&&t.dataset.previousPosition&&(t.style.position=t.dataset.previousPosition,delete t.dataset.previousPosition),i.parentNode&&t.removeChild(i.parentNode)}),300)}),r)}}}}};function O(t){return"undefined"===typeof t||!!t}function L(t){var e={},i=t.currentTarget;i&&i._ripple&&!i._ripple.touched&&(C(t)&&(i._ripple.touched=!0),e.center=i._ripple.centered,i._ripple.class&&(e.class=i._ripple.class),S.show(t,i,e))}function j(t){var e=t.currentTarget;e&&(window.setTimeout((function(){e._ripple&&(e._ripple.touched=!1)})),S.hide(e))}function E(t,e,i){var n=O(e.value);n||S.hide(t),t._ripple=t._ripple||{},t._ripple.enabled=n;var r=e.value||{};r.center&&(t._ripple.centered=!0),r.class&&(t._ripple.class=e.value.class),r.circle&&(t._ripple.circle=r.circle),n&&!i?(t.addEventListener("touchstart",L,{passive:!0}),t.addEventListener("touchend",j,{passive:!0}),t.addEventListener("touchcancel",j),t.addEventListener("mousedown",L),t.addEventListener("mouseup",j),t.addEventListener("mouseleave",j),t.addEventListener("dragstart",j,{passive:!0})):!n&&i&&A(t)}function A(t){t.removeEventListener("mousedown",L),t.removeEventListener("touchstart",j),t.removeEventListener("touchend",j),t.removeEventListener("touchcancel",j),t.removeEventListener("mouseup",j),t.removeEventListener("mouseleave",j),t.removeEventListener("dragstart",j)}function T(t,e,i){E(t,e,!1),i.context&&i.context.$nextTick((function(){var e=window.getComputedStyle(t);if(e&&"inline"===e.display){var n=i.fnOptions?[i.fnOptions,i.context]:[i.componentInstance];d.OP.apply(void 0,["v-ripple can only be used on block-level elements"].concat(n))}}))}function z(t){delete t._ripple,A(t)}function P(t,e){if(e.value!==e.oldValue){var i=O(e.oldValue);E(t,e,i)}}var $={bind:T,unbind:z,update:P},D=Object.assign||function(t){for(var e=1;e<arguments.length;e++){var i=arguments[e];for(var n in i)Object.prototype.hasOwnProperty.call(i,n)&&(t[n]=i[n])}return t};function N(t,e,i){return e in t?Object.defineProperty(t,e,{value:i,enumerable:!0,configurable:!0,writable:!0}):t[e]=i,t}var M=n.Ay.extend({name:"routable",directives:{Ripple:$},props:{activeClass:String,append:Boolean,disabled:Boolean,exact:{type:Boolean,default:void 0},exactActiveClass:String,href:[String,Object],to:[String,Object],nuxt:Boolean,replace:Boolean,ripple:[Boolean,Object],tag:String,target:String},computed:{computedRipple:function(){return!(!this.ripple||this.disabled)&&this.ripple}},methods:{click:function(t){this.$emit("click",t)},generateRouteLink:function(t){var e=this.exact,i=void 0,n=N({attrs:{disabled:this.disabled},class:t,props:{},directives:[{name:"ripple",value:this.computedRipple}]},this.to?"nativeOn":"on",D({},this.$listeners,{click:this.click}));if("undefined"===typeof this.exact&&(e="/"===this.to||this.to===Object(this.to)&&"/"===this.to.path),this.to){var r=this.activeClass,s=this.exactActiveClass||r;this.proxyClass&&(r+=" "+this.proxyClass,s+=" "+this.proxyClass),i=this.nuxt?"nuxt-link":"router-link",Object.assign(n.props,{to:this.to,exact:e,activeClass:r,exactActiveClass:s,append:this.append,replace:this.replace})}else i=(this.href?"a":this.tag)||"a","a"===i&&this.href&&(n.attrs.href=this.href);return this.target&&(n.attrs.target=this.target),{tag:i,data:n}}}});Object.assign;var R=n.Ay.extend().extend({name:"themeable",provide:function(){return{theme:this.themeableProvide}},inject:{theme:{default:{isDark:!1}}},props:{dark:{type:Boolean,default:null},light:{type:Boolean,default:null}},data:function(){return{themeableProvide:{isDark:!1}}},computed:{isDark:function(){return!0===this.dark||!0!==this.light&&this.theme.isDark},themeClasses:function(){return{"theme--dark":this.isDark,"theme--light":!this.isDark}},rootIsDark:function(){return!0===this.dark||!0!==this.light&&this.$vuetify.dark},rootThemeClasses:function(){return{"theme--dark":this.rootIsDark,"theme--light":!this.rootIsDark}}},watch:{isDark:{handler:function(t,e){t!==e&&(this.themeableProvide.isDark=this.isDark)},immediate:!0}}}),I=R;function V(t,e,i){return e in t?Object.defineProperty(t,e,{value:i,enumerable:!0,configurable:!0,writable:!0}):t[e]=i,t}function W(){var t,e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:"value",i=arguments.length>1&&void 0!==arguments[1]?arguments[1]:"input";return n.Ay.extend({name:"toggleable",model:{prop:e,event:i},props:V({},e,{required:!1}),data:function(){return{isActive:!!this[e]}},watch:(t={},V(t,e,(function(t){this.isActive=!!t})),V(t,"isActive",(function(t){!!t!==this[e]&&this.$emit(i,t)})),t)})}W();var H="function"===typeof Symbol&&"symbol"===typeof Symbol.iterator?function(t){return typeof t}:function(t){return t&&"function"===typeof Symbol&&t.constructor===Symbol&&t!==Symbol.prototype?"symbol":typeof t},q=Object.assign||function(t){for(var e=1;e<arguments.length;e++){var i=arguments[e];for(var n in i)Object.prototype.hasOwnProperty.call(i,n)&&(t[n]=i[n])}return t};function X(t,e,i){return e in t?Object.defineProperty(t,e,{value:i,enumerable:!0,configurable:!0,writable:!0}):t[e]=i,t}var Y=r(c,M,k,I,b("btnToggle"),W("inputValue")),F=Y.extend().extend({name:"v-btn",props:{activeClass:{type:String,default:"v-btn--active"},block:Boolean,depressed:Boolean,fab:Boolean,flat:Boolean,icon:Boolean,large:Boolean,loading:Boolean,outline:Boolean,ripple:{type:[Boolean,Object],default:null},round:Boolean,small:Boolean,tag:{type:String,default:"button"},type:{type:String,default:"button"},value:null},computed:{classes:function(){var t;return q((t={"v-btn":!0},X(t,this.activeClass,this.isActive),X(t,"v-btn--absolute",this.absolute),X(t,"v-btn--block",this.block),X(t,"v-btn--bottom",this.bottom),X(t,"v-btn--disabled",this.disabled),X(t,"v-btn--flat",this.flat),X(t,"v-btn--floating",this.fab),X(t,"v-btn--fixed",this.fixed),X(t,"v-btn--icon",this.icon),X(t,"v-btn--large",this.large),X(t,"v-btn--left",this.left),X(t,"v-btn--loader",this.loading),X(t,"v-btn--outline",this.outline),X(t,"v-btn--depressed",this.depressed&&!this.flat||this.outline),X(t,"v-btn--right",this.right),X(t,"v-btn--round",this.round),X(t,"v-btn--router",this.to),X(t,"v-btn--small",this.small),X(t,"v-btn--top",this.top),t),this.themeClasses)},computedRipple:function(){var t=!this.icon&&!this.fab||{circle:!0};return!this.disabled&&(null!==this.ripple?this.ripple:t)}},watch:{$route:"onRouteChange"},methods:{click:function(t){!this.fab&&t.detail&&this.$el.blur(),this.$emit("click",t),this.btnToggle&&this.toggle()},genContent:function(){return this.$createElement("div",{class:"v-btn__content"},this.$slots.default)},genLoader:function(){return this.$createElement("span",{class:"v-btn__loading"},this.$slots.loader||[this.$createElement(h,{props:{indeterminate:!0,size:23,width:2}})])},onRouteChange:function(){var t=this;if(this.to&&this.$refs.link){var e="_vnode.data.class."+this.activeClass;this.$nextTick((function(){(0,g.no)(t.$refs.link,e)&&t.toggle()}))}}},render:function(t){var e=this.outline||this.flat||this.disabled?this.setTextColor:this.setBackgroundColor,i=this.generateRouteLink(this.classes),n=i.tag,r=i.data,s=[this.genContent(),this.loading&&this.genLoader()];return"button"===n&&(r.attrs.type=this.type),r.attrs.value=["string","number"].includes(H(this.value))?this.value:JSON.stringify(this.value),this.btnToggle&&(r.ref="link"),t(n,e(this.color,r),s)}}),G=function(){var t=this,e=t._self._c;return e("div",{staticClass:"about"},[e("h1",[t._v("This is an about page::")]),e(F,[t._v(" Button ")])],1)},J=[],K=i(1656),Q={},U=(0,K.A)(Q,G,J,!1,null,null,null),Z=U.exports}}]);
//# sourceMappingURL=about.8d7fe57e.js.map
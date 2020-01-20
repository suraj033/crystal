﻿/*
 Highcharts JS v7.1.2 (2019-06-03)

 Highcharts funnel module

 (c) 2010-2019 Kacper Madej

 License: www.highcharts.com/license
*/
(function (h) { "object" === typeof module && module.exports ? (h["default"] = h, module.exports = h) : "function" === typeof define && define.amd ? define("highcharts/modules/funnel3d", ["highcharts", "highcharts/highcharts-3d", "highcharts/modules/cylinder"], function (k) { h(k); h.Highcharts = k; return h }) : h("undefined" !== typeof Highcharts ? Highcharts : void 0) })(function (h) {
    function k(d, h, J, k) { d.hasOwnProperty(h) || (d[h] = k.apply(null, J)) } h = h ? h._modules : {}; k(h, "modules/funnel3d.src.js", [h["parts/Globals.js"]], function (d) {
        var h = d.charts,
        k = d.color, K = d.error, E = d.extend, H = d.merge, C = d.pick, x = d.seriesType, L = d.seriesTypes, A = d.relativeLength, m = d.Renderer.prototype, M = m.cuboidPath; x("funnel3d", "column", { center: ["50%", "50%"], width: "90%", neckWidth: "30%", height: "100%", neckHeight: "25%", reversed: !1, gradientForSides: !0, animation: !1, edgeWidth: 0, colorByPoint: !0, showInLegend: !1, dataLabels: { align: "right", crop: !1, inside: !1, overflow: "allow" } }, {
            bindAxes: function () {
                d.Series.prototype.bindAxes.apply(this, arguments); E(this.xAxis.options, {
                    gridLineWidth: 0,
                    lineWidth: 0, title: null, tickPositions: []
                }); E(this.yAxis.options, { gridLineWidth: 0, title: null, labels: { enabled: !1 } })
            }, translate3dShapes: d.noop, translate: function () {
                d.Series.prototype.translate.apply(this, arguments); var a = 0, b = this.chart, c = this.options, g = c.reversed, I = c.ignoreHiddenPoint, e = b.plotWidth, r = b.plotHeight, f = 0, h = c.center, u = A(h[0], e), q = A(h[1], r), k = A(c.width, e), n, t, l = A(c.height, r), m = A(c.neckWidth, e), x = A(c.neckHeight, r), B = q - l / 2 + l - x, e = this.data, y, F, v, z, G, D, p; this.getWidthAt = t = function (b) {
                    var a = q - l /
                        2; return b > B || l === x ? m : m + (k - m) * (1 - (b - a) / (l - x))
                }; this.center = [u, q, l]; this.centerX = u; e.forEach(function (b) { I && !1 === b.visible || (a += b.y) }); e.forEach(function (e) {
                    G = null; y = a ? e.y / a : 0; v = q - l / 2 + f * l; z = v + y * l; n = t(v); D = z - v; p = { gradientForSides: C(e.options.gradientForSides, c.gradientForSides), x: u, y: v, height: D, width: n, z: 1, top: { width: n } }; n = t(z); p.bottom = { fraction: y, width: n }; v >= B ? p.isCylinder = !0 : z > B && (G = z, n = t(B), z = B, p.bottom.width = n, p.middle = { fraction: D ? (B - v) / D : 0, width: n }); g && (p.y = v = q + l / 2 - (f + y) * l, p.middle && (p.middle.fraction =
                        1 - (D ? p.middle.fraction : 0)), n = p.width, p.width = p.bottom.width, p.bottom.width = n); e.shapeArgs = E(e.shapeArgs, p); e.percentage = 100 * y; e.plotX = u; e.plotY = g ? q + l / 2 - (f + y / 2) * l : (v + (G || z)) / 2; F = d.perspective([{ x: u, y: e.plotY, z: g ? -(k - t(e.plotY)) / 2 : -t(e.plotY) / 2 }], b, !0)[0]; e.tooltipPos = [F.x, F.y]; e.dlBoxRaw = { x: u, width: t(e.plotY), y: v, bottom: p.height, fullWidth: k }; I && !1 === e.visible || (f += y)
                })
            }, alignDataLabel: function (a, b, c) {
                var g = a.dlBoxRaw, d = this.chart.inverted, e = a.plotY > C(this.translatedThreshold, this.yAxis.len), r = C(c.inside,
                    !!this.options.stacking), f = { x: g.x, y: g.y, height: 0 }; c.align = C(c.align, !d || r ? "center" : e ? "right" : "left"); c.verticalAlign = C(c.verticalAlign, d || r ? "middle" : e ? "top" : "bottom"); "top" !== c.verticalAlign && (f.y += g.bottom / ("bottom" === c.verticalAlign ? 1 : 2)); f.width = this.getWidthAt(f.y); this.options.reversed && (f.width = g.fullWidth - f.width); r ? f.x -= f.width / 2 : "left" === c.align ? (c.align = "right", f.x -= 1.5 * f.width) : "right" === c.align ? (c.align = "left", f.x += f.width / 2) : f.x -= f.width / 2; a.dlBox = f; L.column.prototype.alignDataLabel.apply(this,
                        arguments)
            }
        }, { shapeType: "funnel3d" }); x = d.merge(m.elements3d.cuboid, {
            parts: "top bottom frontUpper backUpper frontLower backLower rightUpper rightLower".split(" "), mainParts: ["top", "bottom"], sideGroups: ["upperGroup", "lowerGroup"], sideParts: { upperGroup: ["frontUpper", "backUpper", "rightUpper"], lowerGroup: ["frontLower", "backLower", "rightLower"] }, pathType: "funnel3d", opacitySetter: function (a) {
                var b = this, c = b.parts, g = d.charts[b.renderer.chartIndex], h = "group-opacity-" + a + "-" + g.index; b.parts = b.mainParts; b.singleSetterForParts("opacity",
                    a); b.parts = c; g.renderer.filterId || (g.renderer.definition({ tagName: "filter", id: h, children: [{ tagName: "feComponentTransfer", children: [{ tagName: "feFuncA", type: "table", tableValues: "0 " + a }] }] }), b.sideGroups.forEach(function (a) { b[a].attr({ filter: "url(#" + h + ")" }) }), b.renderer.styledMode && (g.renderer.definition({ tagName: "style", textContent: ".highcharts-" + h + " {filter:url(#" + h + ")}" }), b.sideGroups.forEach(function (b) { b.addClass("highcharts-" + h) }))); return b
            }, fillSetter: function (a) {
                var b = this, c = k(a), g = c.rgba[3],
                d = { top: k(a).brighten(.1).get(), bottom: k(a).brighten(-.2).get() }; 1 > g ? (c.rgba[3] = 1, c = c.get("rgb"), b.attr({ opacity: g })) : c = a; c.linearGradient || c.radialGradient || !b.gradientForSides || (c = { linearGradient: { x1: 0, x2: 1, y1: 1, y2: 1 }, stops: [[0, k(a).brighten(-.2).get()], [.5, a], [1, k(a).brighten(-.2).get()]] }); c.linearGradient ? b.sideGroups.forEach(function (a) {
                    var e = b[a].gradientBox, f = c.linearGradient, g = H(c, { linearGradient: { x1: e.x + f.x1 * e.width, y1: e.y + f.y1 * e.height, x2: e.x + f.x2 * e.width, y2: e.y + f.y2 * e.height } }); b.sideParts[a].forEach(function (b) {
                    d[b] =
                        g
                    })
                }) : (H(!0, d, { frontUpper: c, backUpper: c, rightUpper: c, frontLower: c, backLower: c, rightLower: c }), c.radialGradient && b.sideGroups.forEach(function (a) { var c = b[a].gradientBox, e = c.x + c.width / 2, g = c.y + c.height / 2, d = Math.min(c.width, c.height); b.sideParts[a].forEach(function (a) { b[a].setRadialReference([e, g, d]) }) })); b.singleSetterForParts("fill", null, d); b.color = b.fill = a; c.linearGradient && [b.frontLower, b.frontUpper].forEach(function (a) {
                (a = (a = a.element) && b.renderer.gradients[a.gradient]) && "userSpaceOnUse" !== a.attr("gradientUnits") &&
                    a.attr({ gradientUnits: "userSpaceOnUse" })
                }); return b
            }, adjustForGradient: function () { var a = this, b; a.sideGroups.forEach(function (c) { var g = { x: Number.MAX_VALUE, y: Number.MAX_VALUE }, d = { x: -Number.MAX_VALUE, y: -Number.MAX_VALUE }; a.sideParts[c].forEach(function (c) { b = a[c].getBBox(!0); g = { x: Math.min(g.x, b.x), y: Math.min(g.y, b.y) }; d = { x: Math.max(d.x, b.x + b.width), y: Math.max(d.y, b.y + b.height) } }); a[c].gradientBox = { x: g.x, width: d.x - g.x, y: g.y, height: d.y - g.y } }) }, zIndexSetter: function () {
            this.finishedOnAdd && this.adjustForGradient();
                return this.renderer.Element.prototype.zIndexSetter.apply(this, arguments)
            }, onAdd: function () { this.adjustForGradient(); this.finishedOnAdd = !0 }
        }); m.elements3d.funnel3d = x; m.funnel3d = function (a) {
            var b = this.element3d("funnel3d", a), c = { "stroke-width": 1, stroke: "none" }; b.upperGroup = this.g("funnel3d-upper-group").attr({ zIndex: b.frontUpper.zIndex }).add(b);[b.frontUpper, b.backUpper, b.rightUpper].forEach(function (a) { a.attr(c); a.add(b.upperGroup) }); b.lowerGroup = this.g("funnel3d-lower-group").attr({ zIndex: b.frontLower.zIndex }).add(b);
            [b.frontLower, b.backLower, b.rightLower].forEach(function (a) { a.attr(c); a.add(b.lowerGroup) }); b.gradientForSides = a.gradientForSides; return b
        }; m.funnel3dPath = function (a) {
        this.getCylinderEnd || K("A required Highcharts module is missing: cylinder.js", !0, h[this.chartIndex]); var b = h[this.chartIndex], c = a.alphaCorrection = 90 - Math.abs(b.options.chart.options3d.alpha % 180 - 90), g = M.call(this, d.merge(a, { depth: a.width, width: (a.width + a.bottom.width) / 2 })), k = g.isTop, e = !g.isFront, r = !!a.middle, f = this.getCylinderEnd(b, d.merge(a,
            { x: a.x - a.width / 2, z: a.z - a.width / 2, alphaCorrection: c })), m = a.bottom.width, u = d.merge(a, { width: m, x: a.x - m / 2, z: a.z - m / 2, alphaCorrection: c }), q = this.getCylinderEnd(b, u, !0), w = m, n = u, t = q, l = q; r && (w = a.middle.width, n = d.merge(a, { y: a.y + a.middle.fraction * a.height, width: w, x: a.x - w / 2, z: a.z - w / 2 }), t = this.getCylinderEnd(b, n, !1), l = this.getCylinderEnd(b, n, !1)); g = {
                top: f, bottom: q, frontUpper: this.getCylinderFront(f, t), zIndexes: {
                    group: g.zIndexes.group, top: 0 !== k ? 0 : 3, bottom: 1 !== k ? 0 : 3, frontUpper: e ? 2 : 1, backUpper: e ? 1 : 2, rightUpper: e ?
                        2 : 1
                }
            }; g.backUpper = this.getCylinderBack(f, t); f = 1 !== Math.min(w, a.width) / Math.max(w, a.width); g.rightUpper = this.getCylinderFront(this.getCylinderEnd(b, d.merge(a, { x: a.x - a.width / 2, z: a.z - a.width / 2, alphaCorrection: f ? -c : 0 }), !1), this.getCylinderEnd(b, d.merge(n, { alphaCorrection: f ? -c : 0 }), !r)); r && (f = 1 !== Math.min(w, m) / Math.max(w, m), d.merge(!0, g, {
                frontLower: this.getCylinderFront(l, q), backLower: this.getCylinderBack(l, q), rightLower: this.getCylinderFront(this.getCylinderEnd(b, d.merge(u, { alphaCorrection: f ? -c : 0 }), !0),
                    this.getCylinderEnd(b, d.merge(n, { alphaCorrection: f ? -c : 0 }), !1)), zIndexes: { frontLower: e ? 2 : 1, backLower: e ? 1 : 2, rightLower: e ? 1 : 2 }
            })); return g
        }
    }); k(h, "masters/modules/funnel3d.src.js", [], function () { })
});
//# sourceMappingURL=funnel3d.js.map
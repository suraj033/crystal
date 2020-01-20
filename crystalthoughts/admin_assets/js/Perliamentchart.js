﻿/*
  Highcharts JS v7.1.2 (2019-06-03)

 Item series type for Highcharts

 (c) 2019 Torstein Honsi

 License: www.highcharts.com/license
*/
(function (a) { "object" === typeof module && module.exports ? (a["default"] = a, module.exports = a) : "function" === typeof define && define.amd ? define("highcharts/modules/item-series", ["highcharts"], function (b) { a(b); a.Highcharts = b; return a }) : a("undefined" !== typeof Highcharts ? Highcharts : void 0) })(function (a) {
    function b(m, a, b, h) { m.hasOwnProperty(a) || (m[a] = h.apply(null, b)) } a = a ? a._modules : {}; b(a, "modules/item-series.src.js", [a["parts/Globals.js"]], function (a) {
        var b = a.extend, m = a.merge, h = a.seriesTypes.pie.prototype.pointClass.prototype;
        a.seriesType("item", "pie", { endAngle: void 0, innerSize: "40%", itemPadding: .1, layout: "vertical", marker: m(a.defaultOptions.plotOptions.line.marker, { radius: null }), rows: void 0, showInLegend: !0, startAngle: void 0 }, {
            translate: function () { this.slots || (this.slots = []); a.isNumber(this.options.startAngle) && a.isNumber(this.options.endAngle) ? (a.seriesTypes.pie.prototype.translate.call(this), this.slots = this.getSlots()) : this.generatePoints() }, getSlots: function () {
                function a(a) { 0 < C && (a.row.colCount-- , C--) } for (var y = this.center,
                    k = y[2], b = y[3], r, n = this.slots, t, h, u, v, w, g, c, l, q = 0, d, m = this.endAngleRad - this.startAngleRad, x = Number.MAX_VALUE, z, e, A, B = this.options.rows, p = (k - b) / k; x > this.total;)for (z = x, x = n.length = 0, e = A, A = [], q++ , d = k / q / 2, B ? (b = (d - B) / d * k, 0 <= b ? d = B : (b = 0, p = 1)) : d = Math.floor(d * p), r = d; 0 < r; r--)u = (b + r / d * (k - b - q)) / 2, v = m * u, w = Math.ceil(v / q), A.push({ rowRadius: u, rowLength: v, colCount: w }), x += w + 1; if (e) {
                        for (var C = z - this.total; 0 < C;)e.map(function (a) { return { angle: a.colCount / a.rowLength, row: a } }).sort(function (a, f) { return f.angle - a.angle }).slice(0,
                            Math.min(C, Math.ceil(e.length / 2))).forEach(a); e.forEach(function (a) { var f = a.rowRadius; g = (a = a.colCount) ? m / a : 0; for (l = 0; l <= a; l += 1)c = this.startAngleRad + l * g, t = y[0] + Math.cos(c) * f, h = y[1] + Math.sin(c) * f, n.push({ x: t, y: h, angle: c }) }, this); n.sort(function (a, f) { return a.angle - f.angle }); this.itemSize = q; return n
                    }
            }, getRows: function () {
                var a = this.options.rows, b, k; if (!a) if (k = this.chart.plotWidth / this.chart.plotHeight, a = Math.sqrt(this.total), 1 < k) for (a = Math.ceil(a); 0 < a;) { b = this.total / a; if (b / a > k) break; a-- } else for (a = Math.floor(a); a <
                    this.total;) { b = this.total / a; if (b / a < k) break; a++ } return a
            }, drawPoints: function () {
                var f = this, h = this.options, k = f.chart.renderer, m = h.marker, r = this.borderWidth % 2 ? .5 : 1, n = 0, t = this.getRows(), D = Math.ceil(this.total / t), u = this.chart.plotWidth / D, v = this.chart.plotHeight / t, w = this.itemSize || Math.min(u, v); this.points.forEach(function (g) {
                    var c, l, q, d = g.marker || {}, y = d.symbol || m.symbol, d = a.pick(d.radius, m.radius), x = a.defined(d) ? 2 * d : w, z = x * h.itemPadding, e, A, B; g.graphics = l = g.graphics || {}; f.chart.styledMode || (q = f.pointAttribs(g,
                        g.selected && "select")); if (!g.isNull && g.visible) {
                        g.graphic || (g.graphic = k.g("point").add(f.group)); for (var p = 0; p < g.y; p++)f.center && f.slots ? (e = f.slots.shift(), c = e.x - w / 2, e = e.y - w / 2) : "horizontal" === h.layout ? (c = n % D * u, e = v * Math.floor(n / D)) : (c = u * Math.floor(n / t), e = n % t * v), c += z, e += z, B = A = Math.round(x - 2 * z), f.options.crisp && (c = Math.round(c) - r, e = Math.round(e) + r), c = { x: c, y: e, width: A, height: B }, void 0 !== d && (c.r = d), l[p] ? l[p].animate(c) : l[p] = k.symbol(y, null, null, null, null, { backgroundSize: "within" }).attr(b(c, q)).add(g.graphic),
                            l[p].isActive = !0, n++
                        } a.objectEach(l, function (a, b) { a.isActive ? a.isActive = !1 : (a.destroy(), delete l[b]) })
                })
            }, drawDataLabels: function () { this.center && this.slots ? a.seriesTypes.pie.prototype.drawDataLabels.call(this) : this.points.forEach(function (a) { a.destroyElements({ dataLabel: 1 }) }) }, animate: function (a) { a ? this.group.attr({ opacity: 0 }) : (this.group.animate({ opacity: 1 }, this.options.animation), this.animate = null) }
        }, {
            connectorShapes: h.connectorShapes, getConnectorPath: h.getConnectorPath, setVisible: h.setVisible,
                getTranslate: h.getTranslate
            })
    }); b(a, "masters/modules/item-series.src.js", [], function () { })
});
//# sourceMappingURL=item-series.js.map
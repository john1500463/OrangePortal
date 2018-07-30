<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExpeditePage.aspx.cs" Inherits="ExpeditePage" %>
<!DOCTYPE html>




<head id="Head1" runat="server">



   <meta charset='utf-8'>

   <meta http-equiv="X-UA-Compatible" content="IE=edge">

   <meta name="viewport" content="width=device-width, initial-scale=1">

   <link rel="stylesheet" href="styles.css">

   <script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>

   <script src="script.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="Scripts/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>

    <script>(function ($) { var B = register("searchable"); B.defaults = { maxListSize: 100, maxMultiMatch: 50, exactMatch: false, wildcards: true, ignoreCase: true, warnMultiMatch: "top {0} matches ...", warnNoMatch: "no matches ...", latency: 200, zIndex: "auto" }; B.execute = function (g, h) { var j = null; var k = null; var l = null; if ($.browser.msie && parseInt(jQuery.browser.version) < 7) return this; if (this.nodeName != "SELECT" || this.size > 1) return this; var m = $(this); var n = { index: -1, options: null }; var o = "lang"; var p = false; $.browser.chrome = /chrome/.test(navigator.userAgent.toLowerCase()); if ($.browser.chrome) $.browser.safari = false; if ($.meta) { g = $.extend({}, options, m.data()) } var q = $("<div/>"); var r = $("<div/>"); var t = $("<input/>"); var u = $("<select/>"); var x = $("<option>" + g.warnMultiMatch.replace(/\{0\}/g, g.maxMultiMatch) + "</option>").attr("disabled", "true"); var y = $("<option>" + g.warnNoMatch + "</option>").attr("disabled", "true"); var z = { option: function (a) { return $(u.get(0).options[a]) }, selected: function () { return u.find(":selected") }, selectedIndex: function (a) { if (a > -1) u.get(0).selectedIndex = a; return u.get(0).selectedIndex }, size: function (a) { u.attr("size", Math.max(2, Math.min(a, 20))) }, reset: function () { if ((m.get(0).selectedIndex - 1) == m.data("index")) return; var a = m.get(0).selectedIndex; var b = m.get(0).length; var c = Math.floor(g.maxMultiMatch / 2); var d = Math.max(1, (a - c)); var e = Math.min(b, Math.max(g.maxMultiMatch, (a + c))); var f = a - d; u.empty(); this.size(e - d); for (var i = d; i < e; i++) u.append($(m.get(0).options[i]).clone().attr(o, i - 1)); if (e > g.maxMultiMatch) u.append(x); u.get(0).selectedIndex = f } }; draw(); var A = false; r.mouseover(function () { A = true }); r.mouseout(function () { A = false }); u.mouseover(function () { A = true }); u.mouseout(function () { A = false }); t.click(function (e) { if (!p) enable(e, true); else disable(e, true) }); t.blur(function (e) { if (!A && p) disable(e, true) }); m.keydown(function (e) { if (e.keyCode != 9 && !e.shiftKey && !e.ctrlKey && !e.altKey) t.click() }); m.click(function (e) { u.focus() }); u.click(function (e) { if (z.selectedIndex() < 0) return; disable(e) }); u.focus(function (e) { t.focus() }); u.blur(function (e) { if (!A) disable(e, true) }); u.mousemove(function (e) { if ($.browser.opera && parseFloat(jQuery.browser.version) >= 9.8) return true; var a = Math.floor(parseFloat(/([0-9\.]+)px/.exec(z.option(0).css("font-size")))); var b = 4; if ($.browser.opera) b = 2.5; if ($.browser.safari || $.browser.chrome) b = 3; a += Math.round(a / b); z.selectedIndex(Math.floor((e.pageY - u.offset().top + this.scrollTop) / a)) }); r.click(function (e) { t.click() }); t.keyup(function (e) { if (jQuery.inArray(e.keyCode, new Array(9, 13, 16, 33, 34, 35, 36, 38, 40)) > -1) return true; l = $.trim(t.val().toLowerCase()); clearSearchTimer(); j = setTimeout(searching, g.latency) }); t.keydown(function (e) { if (e.keyCode == 9) { disable(e) } if (e.shiftKey || e.ctrlKey || e.altKey) return; switch (e.keyCode) { case 13: disable(e); m.focus(); break; case 27: disable(e, true); m.focus(); break; case 33: if (z.selectedIndex() - u.attr("size") > 0) { z.selectedIndex(z.selectedIndex() - u.attr("size")) } else { z.selectedIndex(0) } synchronize(); break; case 34: if (z.selectedIndex() + u.attr("size") < u.get(0).options.length - 1) { z.selectedIndex(z.selectedIndex() + u.attr("size")) } else { z.selectedIndex(u.get(0).options.length - 1) } synchronize(); break; case 38: if (z.selectedIndex() > 0) { z.selectedIndex(z.selectedIndex() - 1); synchronize() } break; case 40: if (z.selectedIndex() < u.get(0).options.length - 1) { z.selectedIndex(z.selectedIndex() + 1); synchronize() } break; default: return true } return false }); function draw() { m.css("text-decoration", "none"); m.width(m.outerWidth()); m.height(m.outerHeight()); q.css("position", "relative"); q.css("width", m.outerWidth()); if ($.browser.msie) q.css("z-index", h); r.css({ "position": "absolute", "top": 0, "left": 0, "width": m.outerWidth(), "height": m.outerHeight(), "background-color": "#FFFFFF", "opacity": "0.01" }); t.attr("type", "text"); t.hide(); t.height(m.outerHeight()); t.css({ "position": "absolute", "top": 0, "left": 0, "margin": "0px", "padding": "0px", "outline-style": "none", "border-style": "solid", "border-bottom-style": "none", "border-color": "transparent", "background-color": "transparent" }); var a = new Array(); a.push("border-left-width"); a.push("border-top-width"); a.push("font-size"); a.push("font-stretch"); a.push("font-variant"); a.push("font-weight"); a.push("color"); a.push("text-align"); a.push("text-indent"); a.push("text-shadow"); a.push("text-transform"); a.push("padding-left"); a.push("padding-top"); for (var i = 0; i < a.length; i++) t.css(a[i], m.css(a[i])); if ($.browser.msie && parseInt(jQuery.browser.version) < 8) { t.css("padding", "0px"); t.css("padding-left", "3px"); t.css("border-left-width", "2px"); t.css("border-top-width", "3px") } else if ($.browser.chrome) { t.height(m.innerHeight()); t.css("text-transform", "none"); t.css("padding-left", parseFloatPx(t.css("padding-left")) + 3); t.css("padding-top", 2) } else if ($.browser.safari) { t.height(m.innerHeight()); t.css("padding-top", 2); t.css("padding-left", 3); t.css("text-transform", "none") } else if ($.browser.opera) { t.height(m.innerHeight()); var b = parseFloatPx(m.css("padding-left")); t.css("padding-left", b == 1 ? b + 1 : b); t.css("padding-top", 0) } else if ($.browser.mozilla) { t.css("padding-top", "0px"); t.css("border-top", "0px"); t.css("padding-left", parseFloatPx(m.css("padding-left")) + 3) } else { t.css("padding-left", parseFloatPx(m.css("padding-left")) + 3); t.css("padding-top", parseFloatPx(m.css("padding-top")) + 1) } var c = parseFloatPx(m.css("padding-left")) + parseFloatPx(m.css("padding-right")) + parseFloatPx(m.css("border-left-width")) + parseFloatPx(m.css("border-left-width")) + 23; t.width(m.outerWidth() - c); var w = m.css("width"); var d = m.outerWidth(); m.css("width", "auto"); d = d > m.outerWidth() ? d : m.outerWidth(); m.css("width", w); u.hide(); z.size(m.get(0).length); u.css({ "position": "absolute", "top": m.outerHeight(), "left": 0, "width": d, "border": "1px solid #333", "font-weight": "normal", "padding": 0, "background-color": m.css("background-color"), "text-transform": m.css("text-transform") }); var e = /^\d+$/.test(m.css("z-index")) ? m.css("z-index") : 1; if (g.zIndex && /^\d+$/.test(g.zIndex)) e = g.zIndex; r.css("z-index", (e).toString(10)); t.css("z-index", (e + 1).toString(10)); u.css("z-index", (e + 2).toString(10)); m.wrap(q); m.after(r); m.after(t); m.after(u) }; function enable(e, s, v) { if (m.attr("disabled")) return false; m.prepend("<option />"); if (typeof v == "undefined") p = !p; z.reset(); synchronize(); store(); if (s) u.show(); t.show(); t.focus(); t.select(); m.get(0).selectedIndex = 0; if (typeof e != "undefined") e.stopPropagation() }; function disable(e, a) { p = false; m.find(":first").remove(); clearSearchTimer(); t.hide(); u.hide(); if (typeof a != "undefined") restore(); populate(); if (typeof e != "undefined") e.stopPropagation() }; function clearSearchTimer() { if (j != null) clearTimeout(j) }; function populate() { if (z.selectedIndex() < 0 || z.selected().get(0).disabled) return; m.get(0).selectedIndex = parseInt(u.find(":selected").attr(o)); m.change(); m.data("index", new Number(m.get(0).selectedIndex)) }; function synchronize() { if (z.selectedIndex() > -1 && !z.selected().get(0).disabled) t.val(u.find(":selected").text()); else t.val(m.find(":selected").text()) }; function store() { n.index = z.selectedIndex(); n.options = new Array(); for (var i = 0; i < u.get(0).options.length; i++) n.options.push(u.get(0).options[i]) }; function restore() { u.empty(); for (var i = 0; i < n.options.length; i++) u.append(n.options[i]); z.selectedIndex(n.index); z.size(n.options.length) }; function escapeRegExp(a) { var b = ["/", ".", "*", "+", "?", "|", "(", ")", "[", "]", "{", "}", "\\", "^", "$"]; var c = new RegExp("(\\" + b.join("|\\") + ")", "g"); return a.replace(c, "\\$1") }; function searching() { if (k == l) { j = null; return } var a = 0; k = l; u.hide(); u.empty(); var b = escapeRegExp(l); if (g.exactMatch) b = "^" + b; if (g.wildcards) { b = b.replace(/\\\*/g, ".*"); b = b.replace(/\\\?/g, ".") } var c = null; if (g.ignoreCase) c = "i"; l = new RegExp(b, c); for (var i = 1; i < m.get(0).length && a < g.maxMultiMatch; i++) { if (l.length == 0 || l.test(m.get(0).options[i].text)) { var d = $(m.get(0).options[i]).clone().attr(o, i - 1); if (m.data("index") == i) d.text(m.data("text")); u.append(d); a++ } } if (a >= 1) { z.selectedIndex(0) } else if (a == 0) { u.append(y) } if (a >= g.maxMultiMatch) { u.append(x) } z.size(a); u.show(); j = null }; function parseFloatPx(a) { try { a = parseFloat(a.replace(/[\s]*px/, "")); if (!isNaN(a)) return a } catch (e) { } return 0 }; return }; function register(d) { var e = $[d] = {}; $.fn[d] = function (b) { b = $.extend(e.defaults, b); var c = this.size(); return this.each(function (a) { e.execute.call(this, b, c - a) }) }; return e } })(jQuery);</script>
<script type="text/javascript">
    $(document).ready(function () {
        $("select").searchable({
            maxListSize: 200, // if list size are less than maxListSize, show them all
            maxMultiMatch: 300, // how many matching entries should be displayed
            exactMatch: false, // Exact matching on search
            wildcards: true, // Support for wildcard characters (*, ?)
            ignoreCase: true, // Ignore case sensitivity
            latency: 200, // how many millis to wait until starting search
            warnMultiMatch: 'top {0} matches ...',
            warnNoMatch: 'no matches ...',
            zIndex: 'auto'
        });
    });

 </script>
   <title>Home</title>

    <style>

        #cssmenu,

#cssmenu ul,

#cssmenu li,

#cssmenu a {

  border: none;

  line-height: 1;

  margin: 0;

  padding: 0;

}

#cssmenu {

  height: 140px;

  display: block;

  width: 100%;

  border-color: #080808;

  margin: 0;

  padding: 0;

}

#cssmenu > ul {

  list-style: inside none;

  margin: 0;

  padding: 0;

}

#cssmenu > ul > li {

  list-style: inside none;

  display: inline-block;

  position: relative;

  margin: 0;

  padding: 20px;

}

#cssmenu.align-center > ul {

  text-align: center;

}

#cssmenu.align-center > ul > li {

  float: none;

  margin-left: -3px;

}

#cssmenu.align-center ul ul {

  text-align: left;

}

#cssmenu.align-center > ul > li:first-child > a {

  border-radius: 0;

}

#cssmenu > ul > li > a {

  outline: none;

  display: block;

  position: relative;

  text-align: center;

  text-decoration: none;

  text-shadow: 1px 1px 0 rgba(0, 0, 0, 0.4);

  font-weight: 700;

  font-size:  16px;

  font-family: Arial, Helvetica, sans-serif;

  color: #ffffff;

  padding: 12px 20px;

}

#cssmenu > ul > li:first-child > a {

        }

#cssmenu > ul > li > a:after {

  content: "";

  position: absolute;

  top: -1px;

  bottom: -1px;

  right: -2px;

  z-index: 99;

  border-color: #3c3c3c;

}

#cssmenu ul li.has-sub:hover > a:after {

  top: 0;

  bottom: 0;

}

#cssmenu > ul > li.has-sub > a:before {

  content: "";

  position: absolute;

  top: 18px;

  right: 6px;

}

#cssmenu > ul > li.has-sub:hover > a:before {

  top: 19px;

}

#cssmenu > ul > li.has-sub:hover > a {

  padding-bottom: 10px;

  z-index: 999;

  border-color: #3f3f3f;

}

#cssmenu ul li.has-sub:hover > ul,

#cssmenu ul li.has-sub:hover > div {

  display: block;

}

#cssmenu > ul > li.has-sub > a:hover,

#cssmenu > ul > li.has-sub:hover > a {

  background: darkorange;

  border-color: darkorange;

}

#cssmenu ul li > ul,

#cssmenu ul li > div {

  display: none;

  width: auto;

  position: absolute;

  top: 65.5px;

  background: #3f3f3f;

  border-radius: 0 0 5px 5px;

  z-index: 999;

  padding: 10px 0;

}

#cssmenu ul li > ul {

  width: 200px;

}

#cssmenu ul ul ul {

  position: absolute;

}

#cssmenu ul ul li:hover > ul {

  left: 100%;

  top: -10px;

}

#cssmenu ul li > ul li {

  display: block;

  list-style: inside none;

  position: relative;

  margin: 0;

  padding: 0;

}

#cssmenu ul li > ul li a {

  outline: none;

  display: block;

  position: relative;

  font: 10pt Arial, Helvetica, sans-serif;

  color: #ffffff;

  text-decoration: none;

  text-shadow: 1px 1px 0 rgba(0, 0, 0, 0.5);

  margin: 0;

  padding: 8px 20px;

}

#OrangeText

{

  position: relative;

  color: darkorange;

  top:10%;

  margin-left: 0.5%;

  font-size: 38pt;

}

#cssmenu,

#cssmenu ul ul > li:hover > a,

#cssmenu ul ul li a:hover {

  background: #3c3c3c;

  background: -moz-linear-gradient(top, #3c3c3c 0%, #222222 100%);

  background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, #3c3c3c), color-stop(100%, #222222));

  background: -webkit-linear-gradient(top, #3c3c3c 0%, #222222 100%);

  background: -o-linear-gradient(top, #3c3c3c 0%, #222222 100%);

  background: -ms-linear-gradient(top, #3c3c3c 0%, #222222 100%);

  background: linear-gradient(top, #3c3c3c 0%, #222222 100%);

}

#cssmenu > ul > li > a:hover {

  background: darkorange;

  color: #ffffff;

}

#cssmenu ul ul a:hover {

  color: darkorange;

}



img {

    position: relative;

    z-index:0;

    left: 91.5%;

    top: -80%;

    width: 7%;

    min-width:0.05%;

    max-width:50%;

    max-height:110px;

}

body {
overflow:auto;
margin-left:0px;
margin-right:0px;
margin-top:0px;
}

#cssmenu > ul > li.has-sub > a:hover:before {

}



    </style>

</head>

<body>



<div id='cssmenu'>
    
  <div id='OrangeText'>

    Expedite Portal
      </div>

    


<ul>

   <li><a href='Home_Page.aspx'><span>Home</span></a></li>

   <li><a href='My_Expedited_Incidents.aspx'><span>My Expedited Incidents</span></a></li>

   <li class='active has-sub'><a href='#'><span>Expedited Incidents </span></a>

      <ul>

         <li class='has-sub'><a href='Expedited_Incidents.aspx'><span>Expedited Incidents</span></a>

         </li>

         <li class='has-sub'><a href='Sita.aspx'><span>SITA Expedited Incidents</span></a>

         </li>

         <li class='has-sub'><a href='Incidents_to_expedite.aspx'><span>Incidents to Expedite</span></a>

         </li>

         <li class='has-sub'><a href='Expedite_Extraction.aspx'><span>Expedite Extraction</span></a>

         </li>

         <li class='has-sub'><a href='Urgency_Reason_Stats.aspx'><span>Urgency Reason Statistics</span></a>

         </li>

         <li class='has-sub'><a href='CSM_entity.aspx'><span>CSM expedite Incidents</span></a>

         </li>

         <li class='has-sub'><a href='Expedite_multiple_incidents.aspx'><span>Expedite multiple incidents</span></a>

         </li>

      </ul>

   </li>

          <li class='active has-sub'><a href='#'><span>User</span></a>

      <ul>

         <li class='has-sub'><a href='User.aspx'><span>Add New User</span></a>

         </li>

         <li class='has-sub'><a href='ModifyUser.aspx'><span>Modify user</span></a>

         </li>

      </ul>

   </li>
    <li><a href='Urgency_Reasons.aspx'><span>Urgency Reasons</span></a></li>

   <li><a href='#'><span>Help</span></a></li>

   <li class='last' style="z-index:2;"><a href='Default.aspx'><span>Log Out</span></a></li>
    

</ul>

    <img src="download.png">


</div>
    <form id="form2" runat="server">
    <div style="margin-top: 50px;">
        
    
        <p>
       <asp:Label ID="Label1" runat="server" Text="Label" Font-Bold="True"></asp:Label>
            </p>
        <div style="display:inline-block">
        
        <asp:Label ID="Label3" runat="server" Text="Choose Urgency Reason:"></asp:Label>
            </div>
        <div style="display:inline-block">
            <div style="display:block;">
        &nbsp;</div>
            <div style="display:block;">
      <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems = "true">
          
     <asp:ListItem Selected = "True" Text = "Type a Reason .." Value = "Select Reason"></asp:ListItem>
          
      </asp:DropDownList>
        
        </div>
            </div>

<script>
    searchBox = document.querySelector("#searchBox");
    countries = document.querySelector("#DropDownList1");
    var when = "keyup"; //You can change this to keydown, keypress or change

    searchBox.addEventListener("keyup", function (e) {
        var text = e.target.value;
        var options = countries.options;
        for (var i = 0; i < options.length; i++) {
            var option = options[i];
            var optionText = option.text;
            var lowerOptionText = optionText.toLowerCase();
            var lowerText = text.toLowerCase();
            var regex = new RegExp("^" + text, "i");
            var match = optionText.match(regex);
            var contains = lowerOptionText.indexOf(lowerText) != -1;
            if (match || contains) {
                $("#DropDownList1").attr('size', 0);
                option.selected = true;
                return;
            }
            searchBox.selectedIndex = 0;
        }
    });

</script>
        
        <div style="display:inline-block;">  
    <asp:Button ID="Button1" runat="server" Text="Save" OnClick="Button1_Click" />
            
            </div>
        </div>
        
        <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
        <p>
            <asp:Label ID="Label5" runat="server" Text="Add Email to Notify: "></asp:Label>
        <asp:TextBox ID="TextBox_Mail" runat="server"></asp:TextBox>
            <asp:Button ID="Button_Addmail" runat="server" OnClick="Button_Addmail_Click" Text="ADD" />
            </p>
        <div id="theoneformails" runat="server" style="width:400px">
            <p>
            <asp:Label ID="Label_Emailslist" runat="server" Text="Emails to be Notified: " Visible="False"></asp:Label>
               </p>
        </div>
       </form>


<div style="width: 100%; height:15%;background-color: #000000; position: fixed;right: 0;bottom: 0;left: 0;"> <br /> <font color="orange"  center  width="10"><center><b><br />For portal issues, contact us <a style="text-decoration:none; background-color: black;color:lightblue;" href="oniness@orange.com">it.support4business@orange.com</a></b></center></font> </div>

</body>
</html>


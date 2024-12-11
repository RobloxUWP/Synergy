define("ace/mode/lua_highlight_rules",["require","exports","module","ace/lib/oop","ace/mode/text_highlight_rules"], function(require, exports, module) {
"use strict";

var oop = require("../lib/oop");
var TextHighlightRules = require("./text_highlight_rules").TextHighlightRules;

var LuaHighlightRules = function() {

    var keywords = (
        "break|do|else|elseif|end|for|function|if|in|local|repeat|"+
         "return|then|until|while|or|and|not"
    );

	// include and useCompatibility are outside luau and get pre-processed 
	// ai generated list (might be inaccurate!)
    var builtinConstants = ("true|false|Color3|include|useCompatibility|BrickColor|math|ColorSequence|UDim2|table|string|Instance|Vector3|Vector2|CFrame|nil|Enum|_G|game|workspace|_VERSION|UDim|Ray|Region3|Region3int16|Axes|Faces|Rect|NumberRange|NumberSequence|NumberSequenceKeypoint|ColorSequenceKeypoint|Random|TweenInfo|PhysicalProperties|Vector3int16|FloatCurve|Path|PathWaypoint|OverlapParams|RaycastParams|RaycastResult|DebuggerManager|DebuggerConnection|Plugin|PluginGui|DockWidgetPluginGuiInfo|UserSettings|UserGameSettings|RunService|HttpService|TweenService|Players|Lighting|ReplicatedStorage|SoundService|UserInputService|GuiService|TextService|Camera|Terrain|MessagingService|LocalizationService|LogService|Chat|BadgeService|CollectionService|ContextActionService|DataStoreService|Debris|FriendService|GamePassService|GroupService|HapticService|InsertService|JointsService|KeyframeSequenceProvider|MarketplaceService|Mouse|NotificationService|PathfindingService|PointsService|PolicyService|ReplicatedFirst|ScriptContext|Selection|SocialService|StarterGui|StarterPack|StarterPlayer|Stats|Teams|TeleportService|TestService|TextChatService|TimerService|TouchInputService|UserService|VRService|VirtualUser");

    var functions = (
		"string|xpcall|tostring|print|require|" +
		"getfenv|setmetatable|next|assert|tonumber|rawequal|" +
		"collectgarbage|getmetatable|rawset|math|pcall|table|" +
		"type|_G|select|pairs|rawget|ipairs|_VERSION|" +
		"load|error|" +
		
		// ROBLOX SHIT
		"task|GetChildren|GetService|FindFirstChild|WaitForChild|" +
		"Connect|FindFirstChildOfClass|FindFirstAncestorOfClass|" +
		"FindDescendantsOfClass|Clone|Destroy|SetPrimaryPartCFrame|" +
		"Disconnect|GetPropertyChangedSignal|GetAttribute|SetAttribute|" +
		"IsDescendantOf|MoveTo|GetDescendants|FireServer|InvokeServer|" +
		"wait|connect|GetMouse|Workspace|" +
			
		// String Functions
		"sub|upper|len|rep|find|match|char|gmatch|reverse|byte|format|" +
		"gsub|lower|" +
	
		// Math Functions
		"log|max|acos|huge|ldexp|pi|cos|tanh|pow|deg|tan|cosh|sinh|" +
		"random|randomseed|frexp|ceil|floor|rad|abs|sqrt|modf|asin|min|" +
		"mod|fmod|log10|atan2|exp|sin|atan|" +
	
		// Debug and Metatable Functions (Roblox Limited Support)
		"debug|getmetatable|setmetatable|traceback|" +
	
		// Table Functions
		"insert|getn|maxn|concat|sort|remove|" +
	
		// Coroutine Functions
		"resume|yield|status|wrap|create|running|" +
	
		// Metamethods
		"__add|__sub|__mod|__unm|__concat|__lt|__index|__call|__gc|" +
		"__metatable|__mul|__div|__pow|__len|__eq|__le|__newindex|" +
		"__tostring|__mode|__tonumber"
    );

    var stdLibaries = ("string|package|math|debug|table|coroutine");

    var deprecatedIn5152 = ("setn|foreach|foreachi|gcinfo|log10|maxn");

	var uncMethods = (
		// Drawing.md
		"Drawing|cleardrawcache|getrenderproperty|isrenderobj|setrenderproperty|" +
		
		// WebSocket.md
		"WebSocket|" +
		
		// cache.md
		"invalidate|iscached|replace|cloneref|compareinstance|" +
		
		// closures.md
		"checkcaller|clonefunction|getcallingscript|hookfunction|iscclosure|islclosure|isexecutorclosure|loadstring|newcclosure|" +
		
		// console.md
		"rconsoleclear|rconsolecreate|rconsoledestroy|rconsoleinput|rconsoleprint|rconsolesettitle|" +
		
		// crypt.md
		"crypt|" +
		
		// debug.md
		"debug|" +
		
		// filesystem.md
		"readfile|listfiles|writefile|makefolder|appendfile|isfile|isfolder|delfile|delfolder|loadfile|dofile|" +
		
		// input.md
		"isrbxactive|mouse1click|mouse1press|mouse1release|mouse2click|mouse2press|mouse2release|mousemoveabs|mousemoverel|mousescroll|" +
		
		// instances.md
		"fireclickdetector|getcallbackvalue|getconnections|getcustomasset|gethiddenproperty|gethui|getinstances|getnilinstances|isscriptable|sethiddenproperty|setrbxclipboard|setscriptable|" +
		
		// metatable.md
		"getrawmetatable|hookmetamethod|getnamecallmethod|isreadonly|setrawmetatable|setreadonly|" +
		
		// misc.md
		"identifyexecutor|getexecutorname|lz4compress|lz4decompress|messagebox|queue_on_teleport|request|setclipboard|setfpscap|" +
		
		// scripts.md
		"getgc|getgenv|getloadedmodules|getrenv|getrunningscripts|getscriptbytecode|getscriptclosure|getscripthash|getscripts|getsenv|getthreadidentity|setthreadidentity"
	);

    var keywordMapper = this.createKeywordMapper({
        "keyword": keywords,
        "support.function": functions,
        "support.unc": uncMethods,
        "keyword.deprecated": deprecatedIn5152,
        "constant.library": stdLibaries,
        "constant.language": builtinConstants,
        "variable.language": "self"
    }, "identifier");

    var decimalInteger = "(?:(?:[1-9]\\d*)|(?:0))";
    var hexInteger = "(?:0[xX][\\dA-Fa-f]+)";
    var integer = "(?:" + decimalInteger + "|" + hexInteger + ")";

    var fraction = "(?:\\.\\d+)";
    var intPart = "(?:\\d+)";
    var pointFloat = "(?:(?:" + intPart + "?" + fraction + ")|(?:" + intPart + "\\.))";
    var floatNumber = "(?:" + pointFloat + ")";

    this.$rules = {
        "start" : [{
            stateName: "bracketedComment",
            onMatch : function(value, currentState, stack){
                stack.unshift(this.next, value.length - 2, currentState);
                return "comment";
            },
            regex : /\-\-\[=*\[/,
            next  : [
                {
                    onMatch : function(value, currentState, stack) {
                        if (value.length == stack[1]) {
                            stack.shift();
                            stack.shift();
                            this.next = stack.shift();
                        } else {
                            this.next = "";
                        }
                        return "comment";
                    },
                    regex : /\]=*\]/,
                    next  : "start"
                }, {
                    defaultToken : "comment"
                }
            ]
        },

        {
            token : "comment",
            regex : "\\-\\-.*$"
        },
        {
            stateName: "bracketedString",
            onMatch : function(value, currentState, stack){
                stack.unshift(this.next, value.length, currentState);
                return "string.start";
            },
            regex : /\[=*\[/,
            next  : [
                {
                    onMatch : function(value, currentState, stack) {
                        if (value.length == stack[1]) {
                            stack.shift();
                            stack.shift();
                            this.next = stack.shift();
                        } else {
                            this.next = "";
                        }
                        return "string.end";
                    },
                    
                    regex : /\]=*\]/,
                    next  : "start"
                }, {
                    defaultToken : "string"
                }
            ]
        },
        {
            token : "string",           // " string
            regex : '"(?:[^\\\\]|\\\\.)*?"'
        }, {
            token : "string",           // ' string
            regex : "'(?:[^\\\\]|\\\\.)*?'"
        }, {
            token : "constant.numeric", // float
            regex : floatNumber
        }, {
            token : "constant.numeric", // integer
            regex : integer + "\\b"
        }, {
            token : keywordMapper,
            regex : "[a-zA-Z_$][a-zA-Z0-9_$]*\\b"
        }, {
            token : "keyword.operator",
            regex : "\\+|\\-|\\*|\\/|%|\\#|\\^|~|<|>|<=|=>|==|~=|=|\\:|\\.\\.\\.|\\.\\."
        }, {
            token : "paren.lparen",
            regex : "[\\[\\(\\{]"
        }, {
            token : "paren.rparen",
            regex : "[\\]\\)\\}]"
        }, {
            token : "text",
            regex : "\\s+|\\w+"
        } ]
    };
    
    this.normalizeRules();
};

oop.inherits(LuaHighlightRules, TextHighlightRules);

exports.LuaHighlightRules = LuaHighlightRules;
});

define("ace/mode/folding/lua",["require","exports","module","ace/lib/oop","ace/mode/folding/fold_mode","ace/range","ace/token_iterator"], function(require, exports, module) {
"use strict";

var oop = require("../../lib/oop");
var BaseFoldMode = require("./fold_mode").FoldMode;
var Range = require("../../range").Range;
var TokenIterator = require("../../token_iterator").TokenIterator;


var FoldMode = exports.FoldMode = function() {};

oop.inherits(FoldMode, BaseFoldMode);

(function() {

    this.foldingStartMarker = /\b(function|then|do|repeat)\b|{\s*$|(\[=*\[)/;
    this.foldingStopMarker = /\bend\b|^\s*}|\]=*\]/;

    this.getFoldWidget = function(session, foldStyle, row) {
        var line = session.getLine(row);
        var isStart = this.foldingStartMarker.test(line);
        var isEnd = this.foldingStopMarker.test(line);

        if (isStart && !isEnd) {
            var match = line.match(this.foldingStartMarker);
            if (match[1] == "then" && /\belseif\b/.test(line))
                return;
            if (match[1]) {
                if (session.getTokenAt(row, match.index + 1).type === "keyword")
                    return "start";
            } else if (match[2]) {
                var type = session.bgTokenizer.getState(row) || "";
                if (type[0] == "bracketedComment" || type[0] == "bracketedString")
                    return "start";
            } else {
                return "start";
            }
        }
        if (foldStyle != "markbeginend" || !isEnd || isStart && isEnd)
            return "";

        var match = line.match(this.foldingStopMarker);
        if (match[0] === "end") {
            if (session.getTokenAt(row, match.index + 1).type === "keyword")
                return "end";
        } else if (match[0][0] === "]") {
            var type = session.bgTokenizer.getState(row - 1) || "";
            if (type[0] == "bracketedComment" || type[0] == "bracketedString")
                return "end";
        } else
            return "end";
    };

    this.getFoldWidgetRange = function(session, foldStyle, row) {
        var line = session.doc.getLine(row);
        var match = this.foldingStartMarker.exec(line);
        if (match) {
            if (match[1])
                return this.luaBlock(session, row, match.index + 1);

            if (match[2])
                return session.getCommentFoldRange(row, match.index + 1);

            return this.openingBracketBlock(session, "{", row, match.index);
        }

        var match = this.foldingStopMarker.exec(line);
        if (match) {
            if (match[0] === "end") {
                if (session.getTokenAt(row, match.index + 1).type === "keyword")
                    return this.luaBlock(session, row, match.index + 1);
            }

            if (match[0][0] === "]")
                return session.getCommentFoldRange(row, match.index + 1);

            return this.closingBracketBlock(session, "}", row, match.index + match[0].length);
        }
    };

    this.luaBlock = function(session, row, column, tokenRange) {
        var stream = new TokenIterator(session, row, column);
        var indentKeywords = {
            "function": 1,
            "do": 1,
            "then": 1,
            "elseif": -1,
            "end": -1,
            "repeat": 1,
            "until": -1
        };

        var token = stream.getCurrentToken();
        if (!token || token.type != "keyword")
            return;

        var val = token.value;
        var stack = [val];
        var dir = indentKeywords[val];

        if (!dir)
            return;

        var startColumn = dir === -1 ? stream.getCurrentTokenColumn() : session.getLine(row).length;
        var startRow = row;

        stream.step = dir === -1 ? stream.stepBackward : stream.stepForward;
        while(token = stream.step()) {
            if (token.type !== "keyword")
                continue;
            var level = dir * indentKeywords[token.value];

            if (level > 0) {
                stack.unshift(token.value);
            } else if (level <= 0) {
                stack.shift();
                if (!stack.length && token.value != "elseif")
                    break;
                if (level === 0)
                    stack.unshift(token.value);
            }
        }

        if (!token)
            return null;

        if (tokenRange)
            return stream.getCurrentTokenRange();

        var row = stream.getCurrentTokenRow();
        if (dir === -1)
            return new Range(row, session.getLine(row).length, startRow, startColumn);
        else
            return new Range(startRow, startColumn, row, stream.getCurrentTokenColumn());
    };

}).call(FoldMode.prototype);

});

define("ace/mode/lua",["require","exports","module","ace/lib/oop","ace/mode/text","ace/mode/lua_highlight_rules","ace/mode/folding/lua","ace/range","ace/worker/worker_client"], function(require, exports, module) {
"use strict";

var oop = require("../lib/oop");
var TextMode = require("./text").Mode;
var LuaHighlightRules = require("./lua_highlight_rules").LuaHighlightRules;
var LuaFoldMode = require("./folding/lua").FoldMode;
var Range = require("../range").Range;
var WorkerClient = require("../worker/worker_client").WorkerClient;

var Mode = function() {
    this.HighlightRules = LuaHighlightRules;
    
    this.foldingRules = new LuaFoldMode();
    this.$behaviour = this.$defaultBehaviour;
};
oop.inherits(Mode, TextMode);

(function() {
   
    this.lineCommentStart = "--";
    this.blockComment = {start: "--[", end: "]--"};
    
    var indentKeywords = {
        "function": 1,
        "then": 1,
        "do": 1,
        "else": 1,
        "elseif": 1,
        "repeat": 1,
        "end": -1,
        "until": -1
    };
    var outdentKeywords = [
        "else",
        "elseif",
        "end",
        "until"
    ];

    function getNetIndentLevel(tokens) {
        var level = 0;
        for (var i = 0; i < tokens.length; i++) {
            var token = tokens[i];
            if (token.type == "keyword") {
                if (token.value in indentKeywords) {
                    level += indentKeywords[token.value];
                }
            } else if (token.type == "paren.lparen") {
                level += token.value.length;
            } else if (token.type == "paren.rparen") {
                level -= token.value.length;
            }
        }
        if (level < 0) {
            return -1;
        } else if (level > 0) {
            return 1;
        } else {
            return 0;
        }
    }

    this.getNextLineIndent = function(state, line, tab) {
        var indent = this.$getIndent(line);
        var level = 0;

        var tokenizedLine = this.getTokenizer().getLineTokens(line, state);
        var tokens = tokenizedLine.tokens;

        if (state == "start") {
            level = getNetIndentLevel(tokens);
        }
        if (level > 0) {
            return indent + tab;
        } else if (level < 0 && indent.substr(indent.length - tab.length) == tab) {
            if (!this.checkOutdent(state, line, "\n")) {
                return indent.substr(0, indent.length - tab.length);
            }
        }
        return indent;
    };

    this.checkOutdent = function(state, line, input) {
        if (input != "\n" && input != "\r" && input != "\r\n")
            return false;

        if (line.match(/^\s*[\)\}\]]$/))
            return true;

        var tokens = this.getTokenizer().getLineTokens(line.trim(), state).tokens;

        if (!tokens || !tokens.length)
            return false;

        return (tokens[0].type == "keyword" && outdentKeywords.indexOf(tokens[0].value) != -1);
    };

    this.getMatching = function(session, row, column) {
        if (row == undefined) {
            var pos = session.selection.lead;
            column = pos.column;
            row = pos.row;
        }

        var startToken = session.getTokenAt(row, column);
        if (startToken && startToken.value in indentKeywords)
            return this.foldingRules.luaBlock(session, row, column, true);
    };

    this.autoOutdent = function(state, session, row) {
        var line = session.getLine(row);
        var column = line.match(/^\s*/)[0].length;
        if (!column || !row) return;

        var startRange = this.getMatching(session, row, column + 1);
        if (!startRange || startRange.start.row == row)
             return;
        var indent = this.$getIndent(session.getLine(startRange.start.row));
        if (indent.length != column) {
            session.replace(new Range(row, 0, row, column), indent);
            session.outdentRows(new Range(row + 1, 0, row + 1, 0));
        }
    };

    this.createWorker = function(session) {
        var worker = new WorkerClient(["ace"], "ace/mode/lua_worker", "Worker");
        worker.attachToDocument(session.getDocument());
        
        worker.on("annotate", function(e) {
            session.setAnnotations(e.data);
        });
        
        worker.on("terminate", function() {
            session.clearAnnotations();
        });
        
        return worker;
    };

    this.$id = "ace/mode/lua";
    this.snippetFileId = "ace/snippets/lua";
}).call(Mode.prototype);

exports.Mode = Mode;
});                (function() {
                    window.require(["ace/mode/lua"], function(m) {
                        if (typeof module == "object" && typeof exports == "object" && module) {
                            module.exports = m;
                        }
                    });
                })();
            
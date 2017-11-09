



//    jQuery("#grps").jqGrid({
//    url: url,
//        datatype: "json",
//        colNames: ['Index', 'Username', 'Course', 'AssignementDate', 'DueDate', 'State'],
//    colModel: [
//        { name: 'Index', key: true, index: 'Index', width: 55, formatter: "integer", search: true},
//        { name: 'Username', index: 'Username', width: 55 },
//        { name: 'Coursename', index: 'Coursename', width: 100 },
//        { name: 'AssignementDate', index: 'AssignementDate', width: 80, align: "right", formatter: "date" },
//        { name: 'DueDate', index: 'DueDate', width: 80, align: "right", formatter: "date" },
//        { name: 'State', index: 'State', width: 80, align: "right" }
//    ],
//    search: {
//        caption: "Search...",
//        Find: "Find",
//        Reset: "Reset",
//        odata: ['equal', 'not equal', 'less', 'less or equal', 'greater', 'greater or equal', 'begins with', 'does not begin with', 'is in', 'is not in', 'ends with', 'does not end with', 'contains', 'does not contain']
//    },
//    jsonReader: {
//        repeatitems: false,
//        id: "Index",
//        root: 'rows',
//        records: 'records',
//        page: 'page',
//        total: 'total'
//    },
//    rowNum: 10,
//    width: 700,
//    rowList: [10, 20, 30],
//    pager: '#pgrps',
//    sortname: 'invdate',
//    viewrecords: true,
//    sortorder: "desc",
//    caption: "Users",
//    height: '100%'
//});
//jQuery("#grps").jqGrid('navGrid', '#pgrps',
//    { edit: false, add: false, del: false },
//    {},
//    {},
//    {},
//    { multipleSearch: true, multipleGroup: true }
//);
$(function () {
    jQuery("#list485").jqGrid({
        url: "GetJSON",
        datatype: "json",
        height: 'auto',
        width: 800,
        rowNum: 30,
        rowList: [10, 20, 30],
        colNames: ['Index', 'Username', 'Course', 'AssignementDate', 'DueDate', 'State'],
        colModel: [
            { name: 'Index', key: true, index: 'Index', width: 30, formatter: "integer", search: false },
            { name: 'Username', index: 'Username', width: 120, searchoptions:{sopt: ['eq'] }},
            { name: 'Coursename', index: 'Coursename', width: 120, align: "left", searchoptions: { sopt: ['eq'] } },
            { name: 'AssignementDate', index: 'AssignementDate', width: 70, align: "left", formatter: "date", search: false },
            { name: 'DueDate', index: 'DueDate', width: 70, align: "left", formatter: "date", search: false },
            { name: 'State', index: 'State', width: 60, align: "left", searchoptions: { sopt: ['eq'] } }
        ],
        jsonReader: {
            repeatitems: false,
            id: "Index",
            root: 'rows',
            records: 'records',
            page: 'page',
            total: 'total'
        },       
        pager: "#plist485",
        viewrecords: true,
        sortname: 'Username',
        grouping: true,
        groupingView: {
            groupField: ['Username'],
            groupColumnShow: [true],
            groupText: ['<b>{0} - {1} Item(s)</b>'],
            groupCollapse: true,
            groupOrder: ['desc']
        },
        caption: "Information about all users"
    });
    jQuery("#list485").jqGrid('navGrid', '#plist485',
        { edit: false, add: false, del: false },
        {},
        {},
        {},
        { multipleSearch: true, multipleGroup: true }
    );

})


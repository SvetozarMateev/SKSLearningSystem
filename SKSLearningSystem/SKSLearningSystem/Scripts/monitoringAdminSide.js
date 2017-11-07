jQuery("#grps").jqGrid({
    url: '',
    datatype: "json",
    colNames: ['Row','Course', 'AssignementDate', 'Duedate', 'Days'],
    colModel: [
        { name: 'id', key: true, index: 'id', width: 55 },       
        { name: 'Course', index: 'Course', width: 100 },
        { name: 'AssignementDate', index: 'AssignementDate', width: 80, align: "right" },
        { name: 'Duedate', index: 'Duedate', width: 80, align: "right" },
        { name: 'Days', index: 'Days', width: 80, align: "right" }      
    ],
    rowNum: 10,
    width: 700,
    rowList: [10, 20, 30],
    pager: '#pgrps',
    sortname: 'invdate',
    viewrecords: true,
    sortorder: "desc",
    jsonReader: {
        repeatitems: false
    },
    caption: "Complex search",
    height: '100%'
});
jQuery("#grps").jqGrid('navGrid', '#pgrps',
    { edit: false, add: false, del: false },
    {},
    {},
    {},
    { multipleSearch: true, multipleGroup: true }
);
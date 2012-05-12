var M = M || {};

M.shell = {
    meta: {
        templates: {
            shortcuts: '#shortcutsTemplate'
        },
        views: {
            shortcuts: '#taskbar .shortcuts',
            app: '#application'
        }
    },
    init: function (p) {
        var instance = M.shell;
        instance.renderShortcuts();
        instance.loadApp();
    },
    renderShortcuts: function () {
        var instance = M.shell;

        $(instance.meta.templates.shortcuts)
            .tmpl(M.config.user.apps)
            .appendTo(instance.meta.views.shortcuts);
    },
    loadApp: function () {
        // Get the default app
        var defapp = M.util.array.find(M.config.user.apps, 'isdef', 'True');
        M.util.loader.js({
            path: defapp.appurl,
            async: true
        });
    },
    register: function () {
        var instance = M.shell;
        // dothing because this is a single tasking shell
        // returning app root selector
        return instance.meta.views.app;
    }
};
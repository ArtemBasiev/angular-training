export function propertyMap(sourceProperty) {
    return function (target, propertyKey) {
        if (!target.constructor._propertyMap) {
            target.constructor._propertyMap = {};
        }
        target.constructor._propertyMap[propertyKey] = sourceProperty;
    };
}
//# sourceMappingURL=PropertyMap.js.map
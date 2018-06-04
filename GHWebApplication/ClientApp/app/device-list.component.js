var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Input, Component } from '@angular/core';
import { DataService } from './data.service';
import { Info } from './device';
var DeviceListComponent = /** @class */ (function () {
    function DeviceListComponent(dataService) {
        this.dataService = dataService;
    }
    DeviceListComponent.prototype.ngOnInit = function () {
        this.load();
        console.log("ngOnInit()", this.devices);
        //    //this.devices[i].infoClass.fillFromJSON(this.devices[i].info); 
        //}
        //var jsonObj = JSON.parse(this.devices[0].info);
        //for (var propName in jsonObj) {
        //    this.test[propName] = jsonObj[propName];
        //}
    };
    DeviceListComponent.prototype.fillFromJSON = function (json) {
        var jsonObj = JSON.parse(json);
        var info = new Info();
        for (var propName in jsonObj) {
            info[propName] = jsonObj[propName];
            info["is" + propName] = true;
            console.log("info[propName]", info[propName]);
        }
        return info;
    };
    DeviceListComponent.prototype.load = function () {
        var _this = this;
        console.log("метод load()");
        this.dataService.getDevices().subscribe(function (data) {
            _this.devices = data;
            for (var _i = 0, _a = _this.devices; _i < _a.length; _i++) {
                var dev = _a[_i];
                dev.infoClass = _this.fillFromJSON(dev.info);
                var arr = [];
                for (var key in dev.infoClass[0]) {
                    arr.push(key);
                }
                dev.arrInfoKey = arr;
                console.log("dev.arrInfoKey", dev.arrInfoKey);
            }
            console.log("this.devices", _this.devices);
        });
        console.log("this.devices", this.devices);
    };
    DeviceListComponent.prototype.delete = function (id) {
        var _this = this;
        this.dataService.deleteDevice(id).subscribe(function (data) { return _this.load(); });
    };
    __decorate([
        Input(),
        __metadata("design:type", String)
    ], DeviceListComponent.prototype, "room", void 0);
    DeviceListComponent = __decorate([
        Component({
            templateUrl: './device-list.component.html',
            providers: [DataService]
        }),
        __metadata("design:paramtypes", [DataService])
    ], DeviceListComponent);
    return DeviceListComponent;
}());
export { DeviceListComponent };
//# sourceMappingURL=device-list.component.js.map
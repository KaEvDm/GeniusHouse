var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { DataService } from './data.service';
import { Device } from './device';
var DevicesListEdit = /** @class */ (function () {
    function DevicesListEdit(dataService) {
        this.dataService = dataService;
        this.device = new Device(); // изменяемое устройство
        this.tableMode = true; // табличный режим
    }
    DevicesListEdit.prototype.ngOnInit = function () {
        this.loadDevices(); // загрузка данных при старте компонента  
    };
    // получаем данные через сервис
    DevicesListEdit.prototype.loadDevices = function () {
        var _this = this;
        this.dataService.getDevices()
            .subscribe(function (data) { return _this.devices = data; });
    };
    // сохранение данных
    DevicesListEdit.prototype.save = function () {
        var _this = this;
        if (this.device.id == null) {
            this.dataService.createDevice(this.device)
                .subscribe(function (data) {
                console.log(data);
                _this.devices.push(data.body);
            });
        }
        else {
            this.dataService.updateDevice(this.device)
                .subscribe(function (data) { return _this.loadDevices(); });
        }
        this.cancel();
    };
    DevicesListEdit.prototype.editDevice = function (p) {
        this.device = p;
    };
    DevicesListEdit.prototype.cancel = function () {
        this.device = new Device();
        this.tableMode = true;
    };
    DevicesListEdit.prototype.delete = function (p) {
        var _this = this;
        this.dataService.deleteDevice(p.id)
            .subscribe(function (data) { return _this.loadDevices(); });
    };
    DevicesListEdit.prototype.add = function () {
        this.cancel();
        this.tableMode = false;
    };
    DevicesListEdit = __decorate([
        Component({
            templateUrl: './devices-list-edit.component.html',
            providers: [DataService]
        }),
        __metadata("design:paramtypes", [DataService])
    ], DevicesListEdit);
    return DevicesListEdit;
}());
export { DevicesListEdit };
//# sourceMappingURL=devices-list-edit.component.js.map
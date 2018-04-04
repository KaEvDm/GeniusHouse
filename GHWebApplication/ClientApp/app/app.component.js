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
var AppComponent = /** @class */ (function () {
    function AppComponent(dataService) {
        this.dataService = dataService;
        this.device = new Device(); // изменяемое устройство
        this.tableMode = true; // табличный режим
    }
    AppComponent.prototype.ngOnInit = function () {
        this.loadDevices(); // загрузка данных при старте компонента  
    };
    // получаем данные через сервис
    AppComponent.prototype.loadDevices = function () {
        var _this = this;
        this.dataService.getDevices()
            .subscribe(function (data) { return _this.devices = data; });
    };
    // сохранение данных
    AppComponent.prototype.save = function () {
        var _this = this;
        if (this.device.id == null) {
            this.dataService.createDevice(this.device)
                .subscribe(function (data) { return _this.devices.push(data); });
        }
        else {
            this.dataService.updateDevice(this.device)
                .subscribe(function (data) { return _this.loadDevices(); });
        }
        this.cancel();
    };
    AppComponent.prototype.editProduct = function (p) {
        this.device = p;
    };
    AppComponent.prototype.cancel = function () {
        this.device = new Device();
        this.tableMode = true;
    };
    AppComponent.prototype.delete = function (p) {
        var _this = this;
        this.dataService.deleteDevice(p.id)
            .subscribe(function (data) { return _this.loadDevices(); });
    };
    AppComponent.prototype.add = function () {
        this.cancel();
        this.tableMode = false;
    };
    AppComponent = __decorate([
        Component({
            selector: 'app',
            templateUrl: './app.component.html',
            providers: [DataService]
        }),
        __metadata("design:paramtypes", [DataService])
    ], AppComponent);
    return AppComponent;
}());
export { AppComponent };
//# sourceMappingURL=app.component.js.map
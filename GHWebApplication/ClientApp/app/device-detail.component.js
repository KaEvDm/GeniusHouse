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
import { ActivatedRoute } from '@angular/router';
import { DataService } from './data.service';
var DeviceDetailComponent = /** @class */ (function () {
    function DeviceDetailComponent(dataService, activeRoute) {
        this.dataService = dataService;
        this.loaded = false;
        this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
    }
    DeviceDetailComponent.prototype.ngOnInit = function () {
        var _this = this;
        if (this.id)
            this.dataService.getDevice(this.id)
                .subscribe(function (data) { _this.device = data; _this.loaded = true; });
    };
    DeviceDetailComponent = __decorate([
        Component({
            templateUrl: './device-detail.component.html',
            providers: [DataService]
        }),
        __metadata("design:paramtypes", [DataService, ActivatedRoute])
    ], DeviceDetailComponent);
    return DeviceDetailComponent;
}());
export { DeviceDetailComponent };
//# sourceMappingURL=device-detail.component.js.map
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
import { Router, ActivatedRoute } from '@angular/router';
import { DataService } from './data.service';
var DeviceListRoomComponent = /** @class */ (function () {
    function DeviceListRoomComponent(dataService, router, activeRoute) {
        this.dataService = dataService;
        this.router = router;
        this.room = activeRoute.snapshot.params["room"];
    }
    DeviceListRoomComponent.prototype.ngOnInit = function () {
        this.load();
    };
    DeviceListRoomComponent.prototype.load = function () {
        var _this = this;
        if (this.room)
            this.dataService.getDevicesSort(this.room)
                .subscribe(function (data) { return _this.devices = data; });
    };
    DeviceListRoomComponent = __decorate([
        Component({
            templateUrl: './device-list-room.component.html',
            providers: [DataService]
        }),
        __metadata("design:paramtypes", [DataService, Router, ActivatedRoute])
    ], DeviceListRoomComponent);
    return DeviceListRoomComponent;
}());
export { DeviceListRoomComponent };
//# sourceMappingURL=device-list-room.component.js.map
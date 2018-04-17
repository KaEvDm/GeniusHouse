var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { DeviceListComponent } from './device-list.component';
import { DeviceFormComponent } from './device-form.component';
import { DeviceCreateComponent } from './device-create.component';
import { DeviceEditComponent } from './device-edit.component';
import { NotFoundComponent } from './not-found.component';
import { DeviceListRoomComponent } from './device-list-room.component';
import { DataService } from './data.service';
// определение маршрутов
var appRoutes = [
    { path: '', component: DeviceListComponent },
    { path: 'selectroom/:room', component: DeviceListRoomComponent },
    { path: 'create', component: DeviceCreateComponent },
    { path: 'edit/:id', component: DeviceEditComponent },
    { path: '**', component: NotFoundComponent }
];
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        NgModule({
            imports: [BrowserModule, FormsModule, HttpClientModule, RouterModule.forRoot(appRoutes)],
            declarations: [AppComponent, DeviceListComponent, DeviceCreateComponent, DeviceEditComponent,
                DeviceFormComponent, NotFoundComponent, DeviceListRoomComponent],
            providers: [DataService],
            bootstrap: [AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
export { AppModule };
//# sourceMappingURL=app.module.js.map
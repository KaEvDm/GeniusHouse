import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { DeviceDetailComponent } from './device-detail.component';
import { DeviceListComponent } from './device-list.component';
import { DevicesListEdit } from './devices-list-edit.component';

// определение маршрутов
const appRoutes: Routes = [
    { path: '', component: DeviceListComponent },
    { path: 'device/:id', component: DeviceDetailComponent },
    { path: 'Редактировать', component: DevicesListEdit },
    { path: '**', redirectTo: '/' }
];

@NgModule({
    imports: [BrowserModule, FormsModule, HttpClientModule, RouterModule.forRoot(appRoutes)],
    declarations: [AppComponent, DeviceListComponent, DevicesListEdit, DeviceDetailComponent],
    bootstrap: [AppComponent]
})

export class AppModule { }
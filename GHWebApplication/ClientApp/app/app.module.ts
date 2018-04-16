import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';


import { AppComponent } from './app.component';
import { DeviceListComponent } from './device-list.component';
import { DeviceFormComponent } from './device-form.component';
import { DeviceCreateComponent } from './device-create.component';
import { DeviceEditComponent } from './device-edit.component';
import { NotFoundComponent } from './not-found.component';

import { DataService } from './data.service';
import { DevicesListEdit } from './devices-list-edit.component';

// определение маршрутов
const appRoutes: Routes = [
    { path: '', component: DeviceListComponent },
    { path: 'create', component: DeviceCreateComponent },
    { path: 'edit/:id', component: DeviceEditComponent },
    { path: '**', component: NotFoundComponent }
];

@NgModule({
    imports: [BrowserModule, FormsModule, HttpClientModule, RouterModule.forRoot(appRoutes)],
    declarations: [AppComponent, DeviceListComponent, DeviceCreateComponent, DeviceEditComponent,
        DeviceFormComponent, NotFoundComponent],
    providers: [DataService], // регистрация сервисов
    bootstrap: [AppComponent]
})

export class AppModule { }
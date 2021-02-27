import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { StatModule } from '../../shared/modules/stat/stat.module';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { MaterialModule } from 'src/app/shared/modules/material/material.module';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';

@NgModule({
    imports: [
        CommonModule,
        DashboardRoutingModule,
        StatModule,
        MaterialModule,
        FormsModule,
        ReactiveFormsModule,
        FlexLayoutModule.withConfig({ addFlexToParent: false }),
        NgxMaterialTimepickerModule,
        MatDialogModule
    ],
    declarations: [DashboardComponent]
})
export class DashboardModule { }

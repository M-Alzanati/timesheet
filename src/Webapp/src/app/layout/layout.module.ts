import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { MaterialModule } from '../shared/modules/material/material.module';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { TopnavComponent } from './components/topnav/topnav.component';
import { LayoutRoutingModule } from './layout-routing.module';
import { LayoutComponent } from './layout.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { StatModule } from '../shared/modules/stat/stat.module';

@NgModule({
    imports: [
        CommonModule,
        LayoutRoutingModule,
        MaterialModule,
        TranslateModule,
        MatDatepickerModule,
        StatModule,
    ],
    declarations: [
        LayoutComponent,
        TopnavComponent,
        SidebarComponent
    ]

})
export class LayoutModule { }

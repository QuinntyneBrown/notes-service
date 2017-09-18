import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { SharedModule } from "../shared/shared.module";
import { UsersModule } from "../users/users.module";

import { AuthGuardService } from "../shared/guards/auth-guard.service";
import { TenantGuardService } from "../shared/guards/tenant-guard.service";
import { EventHubConnectionGuardService } from "../shared/guards/event-hub-connection-guard.service";
import { CurrentUserGuardService } from "../users/current-user-guard.service";

import { TagsService } from "./tags.service";

import { TagEditComponent } from "./tag-edit.component";
import { TagEditPageComponent } from "./tag-edit-page.component";
import { TagListItemComponent } from "./tag-list-item.component";
import { TagPaginatedListComponent } from "./tag-paginated-list.component";
import { TagPaginatedListPageComponent } from "./tag-paginated-list-page.component";
import { TagsLeftNavComponent } from "./tags-left-nav.component";

export const TAG_ROUTES: Routes = [{
    path: 'tags',
    component: TagPaginatedListPageComponent,
    canActivate: [
        TenantGuardService,
        AuthGuardService,
        EventHubConnectionGuardService,
        CurrentUserGuardService
    ]
},
{
    path: 'tags/create',
    component: TagEditPageComponent,
    canActivate: [
        TenantGuardService,
        AuthGuardService,
        EventHubConnectionGuardService,
        CurrentUserGuardService
    ]
},
{
    path: 'tags/:id',
    component: TagEditPageComponent,
    canActivate: [
        TenantGuardService,
        AuthGuardService,
        EventHubConnectionGuardService,
        CurrentUserGuardService
    ]
}];

const declarables = [
    TagEditComponent,
    TagEditPageComponent,
    TagListItemComponent,
    TagPaginatedListComponent,
    TagPaginatedListPageComponent,
    TagsLeftNavComponent
];

const providers = [TagsService];

@NgModule({
    imports: [CommonModule, FormsModule, HttpClientModule, ReactiveFormsModule, RouterModule.forChild(TAG_ROUTES), SharedModule, UsersModule],
    exports: [declarables],
    declarations: [declarables],
    providers: providers
})
export class TagsModule { }

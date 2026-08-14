export interface AuditLogDetailsModel {
  id: number;

  userId: number;
  username: string;
  name: string;

  entityId: number;
  entityName: string;

  operationTypeId: number;
  operationTypeName: string;

  dateTime: Date;
}

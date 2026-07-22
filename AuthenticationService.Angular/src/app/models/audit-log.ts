export interface AuditLog {
  id: number;
  userID: number;
  username: string;
  name: string;

  entityID: number;
  entityName: string;

  operationTypeID: number;
  operationTypeName: string;

  dateTime: Date;
}

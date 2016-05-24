﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Universal.BusinessEntities;
using System.Collections;
using System.Data.SqlClient;


namespace Universal.DataAccessLayer
{
    public class UserDA
    {

        public int? BlockDates(ExpenseBlockedDates blockeddates)
        {
            ArrayList paramlv = null;
            int? UpdatedResult = null;

            try
            {
                paramlv = new ArrayList();

                SqlParameter blockedStatus = new SqlParameter();
                blockedStatus.ParameterName = "@Status";
                if (blockeddates.Status != null)
                {
                    blockedStatus.Value = blockeddates.Status;
                    blockedStatus.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    blockedStatus.Value = null;
                    blockedStatus.SqlDbType = SqlDbType.NVarChar;
                }
                paramlv.Add(blockedStatus);


                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramlv.Add(errorCount);


                if (blockeddates.Id != null)
                {
                    SqlParameter Id = new SqlParameter();
                    Id.ParameterName = "@Id";
                    if (blockeddates.Id != null)
                    {
                        Id.Value = blockeddates.Id;
                        Id.SqlDbType = SqlDbType.Int;
                    }
                    else
                    {
                        Id.Value = null;
                        Id.SqlDbType = SqlDbType.Int;
                    }
                    paramlv.Add(Id);


                    SqlParameter ModifiedBy = new SqlParameter();
                    ModifiedBy.ParameterName = "@ModifiedBy";
                    if (blockeddates.ModifiedBy != null)
                    {
                        ModifiedBy.Value = blockeddates.ModifiedBy;
                        ModifiedBy.SqlDbType = SqlDbType.NVarChar;
                    }
                    else
                    {
                        ModifiedBy.Value = null;
                        ModifiedBy.SqlDbType = SqlDbType.NVarChar;
                    }
                    paramlv.Add(ModifiedBy);


                    SqlParameter comment = new SqlParameter();
                    comment.ParameterName = "@Comments";
                    if (blockeddates.Comments != null)
                    {
                        comment.Value = blockeddates.Comments;
                        comment.SqlDbType = SqlDbType.NVarChar;
                    }
                    else
                    {
                        comment.Value = null;
                        comment.SqlDbType = SqlDbType.NVarChar;
                    }
                    paramlv.Add(comment);


                    SqlParameter ModifiedDate = new SqlParameter();
                    ModifiedDate.ParameterName = "@ModifiedDate";
                    ModifiedDate.Value = DateTime.UtcNow.AddHours(5.5);
                    ModifiedDate.SqlDbType = SqlDbType.DateTime;
                    paramlv.Add(ModifiedDate);

                    GDSDBWrapper.ExecuteNonQueryOutParam("SP_UpdateBlockDates", paramlv);


                }
                else
                {
                    SqlParameter empid = new SqlParameter();
                    empid.ParameterName = "@EmpId";
                    if (blockeddates.EmpId != null)
                    {
                        empid.Value = blockeddates.EmpId;
                        empid.SqlDbType = SqlDbType.Int;
                    }
                    else
                    {
                        empid.Value = null;
                        empid.SqlDbType = SqlDbType.Int;
                    }
                    paramlv.Add(empid);


                    SqlParameter CreatedBy = new SqlParameter();
                    CreatedBy.ParameterName = "@CreatedBy";

                    CreatedBy.Value = "Scheduler";
                    CreatedBy.SqlDbType = SqlDbType.NVarChar;
                    paramlv.Add(CreatedBy);


                    SqlParameter blockedfromdate = new SqlParameter();
                    blockedfromdate.ParameterName = "@BlockingFromDate";
                    if (blockeddates.BlockingFromDate != null)
                    {
                        blockedfromdate.Value = blockeddates.BlockingFromDate;
                        blockedfromdate.SqlDbType = SqlDbType.DateTime;
                    }
                    else
                    {
                        blockedfromdate.Value = null;
                        blockedfromdate.SqlDbType = SqlDbType.DateTime;
                    }
                    paramlv.Add(blockedfromdate);

                    SqlParameter blockedtodate = new SqlParameter();
                    blockedtodate.ParameterName = "@BlockingToDate";
                    if (blockeddates.BlockingToDate != null)
                    {
                        blockedtodate.Value = blockeddates.BlockingToDate;
                        blockedtodate.SqlDbType = SqlDbType.DateTime;
                    }
                    else
                    {
                        blockedtodate.Value = null;
                        blockedtodate.SqlDbType = SqlDbType.DateTime;
                    }
                    paramlv.Add(blockedtodate);

                    SqlParameter BlockingType = new SqlParameter();
                    BlockingType.ParameterName = "@BlockingType";
                    BlockingType.Value = blockeddates.BlockingType;
                    BlockingType.SqlDbType = SqlDbType.VarChar;
                    paramlv.Add(BlockingType);


                    SqlParameter CreatedDate = new SqlParameter();
                    CreatedDate.ParameterName = "@CreatedDate";
                    CreatedDate.Value = DateTime.Now;
                    CreatedDate.SqlDbType = SqlDbType.DateTime;
                    paramlv.Add(CreatedDate);
                    GDSDBWrapper.ExecuteNonQueryOutParam("SP_InsertBlockDates", paramlv);

                }
                UpdatedResult = int.Parse(errorCount.Value.ToString());
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return UpdatedResult;
        }


        public DataTable ValidateUser(Employees emp)
        {
            DataTable user = null;
            ArrayList paramRequest = null;
            try
            {
                paramRequest = new ArrayList();
                if (!string.IsNullOrEmpty(emp.Employee_ID))
                {
                    SqlParameter parEmpId = new SqlParameter();
                    parEmpId.ParameterName = "@Empid";
                    parEmpId.Value = emp.Employee_ID;
                    parEmpId.SqlDbType = SqlDbType.VarChar;
                    paramRequest.Add(parEmpId);
                }

                if (!string.IsNullOrEmpty(emp.Password))
                {
                    SqlParameter parPassword = new SqlParameter();
                    parPassword.ParameterName = "@Password";
                    parPassword.Value = emp.Password;
                    parPassword.SqlDbType = SqlDbType.VarChar;
                    paramRequest.Add(parPassword);
                }


                if (paramRequest.Count > 0)

                    user = GDSDBWrapper.ExecuteDataReaderMultiParam("sp_ValidateUser", paramRequest);


            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return user;
        }

        public DataTable GetEmployees()
        {
            DataTable dtEmployees = new DataTable();

            try
            {
                ArrayList emps = new ArrayList();

                dtEmployees = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetEmployees", emps);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtEmployees;
        }

        public DataTable GetLeaves(int? userid, int? managerid)
        {
            DataTable dtLeaves = new DataTable();
            ArrayList paramColl = new ArrayList();
            try
            {
                if (userid != null)
                {


                    SqlParameter spId = new SqlParameter();
                    spId.ParameterName = "@userid";
                    if (userid != null)
                    {
                        spId.Value = userid;
                        spId.SqlDbType = SqlDbType.Int;
                    }
                    else
                    {
                        spId.Value = null;
                        spId.SqlDbType = SqlDbType.Int;
                    }

                    paramColl.Add(spId);

                    dtLeaves = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetLeavesByUserID", paramColl);
                }
                else if (managerid != null)
                {

                    dtLeaves = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetLeaves", paramColl);
                }
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtLeaves;
        }

        public DataTable GetAllLeaves(DateTime LeaveSDate, DateTime LeaveEDate)
        {
            DataTable dtAllLeaves = new DataTable();
            ArrayList paramColl = null;
            try
            {
               #region LeaveSDate
                paramColl = new ArrayList();
                SqlParameter spMaxDate = new SqlParameter();
                spMaxDate.ParameterName = "@LeaveSDate";
                if (LeaveSDate != null)
                {
                    spMaxDate.Value = LeaveSDate;
                    spMaxDate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spMaxDate.Value = null;
                    spMaxDate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spMaxDate);
                #endregion

                #region LeaveEDate
                SqlParameter spEDate = new SqlParameter();
                spEDate.ParameterName = "@LeaveEDate";
                if (LeaveEDate != null)
                {
                    spEDate.Value = LeaveEDate;
                    spEDate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spEDate.Value = null;
                    spEDate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spEDate);
                #endregion

                dtAllLeaves = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetAllLeaves", paramColl);

            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtAllLeaves;
        }

        public int? CreateUserGroup(UserGroups usergroup)
        {
            ArrayList paramColl = null;
            int? result = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter spgroupId = new SqlParameter();
                spgroupId.ParameterName = "@UserGroupId";
                if ((usergroup.UserGroup_ID) != null)
                {
                    spgroupId.Value = usergroup.UserGroup_ID;
                    spgroupId.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spgroupId.Value = null;
                    spgroupId.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spgroupId);

                SqlParameter spgroupDesc = new SqlParameter();
                spgroupDesc.ParameterName = "@Description";
                if ((usergroup.Description) != null)
                {
                    spgroupDesc.Value = usergroup.Description;
                    spgroupDesc.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spgroupDesc.Value = null;
                    spgroupDesc.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spgroupDesc);
                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramColl.Add(errorCount);

                if (usergroup.Id != null)
                {
                    SqlParameter spid = new SqlParameter();
                    spid.ParameterName = "@Id";
                    spid.Value = usergroup.Id;
                    spid.SqlDbType = SqlDbType.Int;
                    paramColl.Add(spid);

                    SqlParameter spUpdateBy = new SqlParameter();
                    spUpdateBy.ParameterName = "@ModifiedBy";
                    spUpdateBy.Value = usergroup.ModifiedBy;
                    spUpdateBy.SqlDbType = SqlDbType.NVarChar;
                    paramColl.Add(spUpdateBy);

                    GDSDBWrapper.ExecuteNonQueryOutParam("sp_UpdateUserGroup", paramColl);
                }
                else
                {
                    SqlParameter spCreatedBy = new SqlParameter();
                    spCreatedBy.ParameterName = "@CreatedBy";
                    spCreatedBy.Value = usergroup.CreatedBy;
                    spCreatedBy.SqlDbType = SqlDbType.NVarChar;
                    paramColl.Add(spCreatedBy);
                    GDSDBWrapper.ExecuteNonQueryOutParam("sp_CreateUserGroup", paramColl);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }

        public int? CreateTravel(TravelDesk travelDesk, int flag)
        {
            ArrayList paramColl = null;
            int? result = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter spfkempId = new SqlParameter();
                spfkempId.ParameterName = "@FK_EmployeeId";
                if ((travelDesk.FK_EmployeeId) != null)
                {
                    spfkempId.Value = travelDesk.FK_EmployeeId;
                    spfkempId.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spfkempId.Value = null;
                    spfkempId.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spfkempId);


                SqlParameter spfromdate = new SqlParameter();
                spfromdate.ParameterName = "@TraveledFromDate";
                if ((travelDesk.TraveledFromDate) != null)
                {
                    spfromdate.Value = travelDesk.TraveledFromDate;
                    spfromdate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spfromdate.Value = null;
                    spfromdate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spfromdate);


                SqlParameter sptodate = new SqlParameter();
                sptodate.ParameterName = "@TraveledToDate";
                if ((travelDesk.TraveledToDate) != null)
                {
                    sptodate.Value = travelDesk.TraveledToDate;
                    sptodate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    sptodate.Value = null;
                    sptodate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(sptodate);

                SqlParameter spfromLoc = new SqlParameter();
                spfromLoc.ParameterName = "@FromLocation";
                if ((travelDesk.FromLocation) != null)
                {
                    spfromLoc.Value = travelDesk.FromLocation;
                    spfromLoc.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spfromLoc.Value = null;
                    spfromLoc.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spfromLoc);



                SqlParameter sptoLoc = new SqlParameter();
                sptoLoc.ParameterName = "@ToLocation";
                if ((travelDesk.ToLocation) != null)
                {
                    sptoLoc.Value = travelDesk.ToLocation;
                    sptoLoc.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    sptoLoc.Value = null;
                    sptoLoc.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(sptoLoc);



                SqlParameter spadv = new SqlParameter();
                spadv.ParameterName = "@AdvanceAmount";
                if ((travelDesk.AdvanceAmount) != null)
                {
                    spadv.Value = travelDesk.AdvanceAmount;
                    spadv.SqlDbType = SqlDbType.Float;
                }
                else
                {
                    spadv.Value = null;
                    spadv.SqlDbType = SqlDbType.Float;
                }
                paramColl.Add(spadv);


                SqlParameter sppurpose = new SqlParameter();
                sppurpose.ParameterName = "@Purpose";
                if ((travelDesk.Purpose) != null)
                {
                    sppurpose.Value = travelDesk.Purpose;
                    sppurpose.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    sppurpose.Value = null;
                    sppurpose.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(sppurpose);


                SqlParameter spMode = new SqlParameter();
                spMode.ParameterName = "@ModeofTransport";
                if ((travelDesk.ModeofTransport) != null)
                {
                    spMode.Value = travelDesk.ModeofTransport;
                    spMode.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spMode.Value = null;
                    spMode.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spMode);


                SqlParameter spModifiedby = new SqlParameter();
                spModifiedby.ParameterName = "@ModifiedBy";
                if ((travelDesk.ModifiedBy) != null)
                {
                    spModifiedby.Value = travelDesk.ModifiedBy;
                    spModifiedby.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spModifiedby.Value = null;
                    spModifiedby.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spModifiedby);



                SqlParameter spModifieddate = new SqlParameter();
                spModifieddate.ParameterName = "@ModifiedDate";

                spModifieddate.Value = DateTime.Now;
                spModifieddate.SqlDbType = SqlDbType.DateTime;


                paramColl.Add(spModifieddate);


                SqlParameter sphotelDetail = new SqlParameter();
                sphotelDetail.ParameterName = "@HotelDetails";
                if ((travelDesk.HotelDetails) != null)
                {
                    sphotelDetail.Value = travelDesk.HotelDetails;
                    sphotelDetail.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    sphotelDetail.Value = null;
                    sphotelDetail.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(sphotelDetail);



                SqlParameter sphotelamount = new SqlParameter();
                sphotelamount.ParameterName = "@HotelBookingAmount";
                if ((travelDesk.HotelBookingAmount) != null)
                {
                    sphotelamount.Value = travelDesk.HotelBookingAmount;
                    sphotelamount.SqlDbType = SqlDbType.Float;
                }
                else
                {
                    sphotelamount.Value = null;
                    sphotelamount.SqlDbType = SqlDbType.Float;
                }
                paramColl.Add(sphotelamount);

                SqlParameter sptotalamount = new SqlParameter();
                sptotalamount.ParameterName = "@TicketAmount";
                if ((travelDesk.TotalTicketAmount) != null)
                {
                    sptotalamount.Value = travelDesk.TotalTicketAmount;
                    sptotalamount.SqlDbType = SqlDbType.Float;
                }
                else
                {
                    sptotalamount.Value = null;
                    sptotalamount.SqlDbType = SqlDbType.Float;
                }
                paramColl.Add(sptotalamount);

                SqlParameter checkedindate = new SqlParameter();
                checkedindate.ParameterName = "@Checkedindate";
                if ((travelDesk.CheckedIndate) != null)
                {
                    checkedindate.Value = travelDesk.CheckedIndate;
                    checkedindate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    checkedindate.Value = null;
                    checkedindate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(checkedindate);

                SqlParameter checkedoutdate = new SqlParameter();
                checkedoutdate.ParameterName = "@Checkedoutdate";
                if ((travelDesk.CheckedIndate) != null)
                {
                    checkedoutdate.Value = travelDesk.CheckedOutdate;
                    checkedoutdate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    checkedoutdate.Value = null;
                    checkedoutdate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(checkedoutdate);

                SqlParameter comments = new SqlParameter();
                comments.ParameterName = "@OverAllComments";
                if ((travelDesk.OverallComments) != null)
                {
                    comments.Value = travelDesk.OverallComments;
                    comments.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    comments.Value = null;
                    comments.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(comments);


                SqlParameter spMode1 = new SqlParameter();
                spMode1.ParameterName = "@ModeofTransportFrom";
                if ((travelDesk.ModeofTransportFrom) != null)
                {
                    spMode1.Value = travelDesk.ModeofTransportFrom;
                    spMode1.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spMode1.Value = null;
                    spMode1.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spMode1);


                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramColl.Add(errorCount);

                if (travelDesk.Id == null && flag == 1)
                {
                    SqlParameter spCreatedby = new SqlParameter();
                    spCreatedby.ParameterName = "@CreatedBy";
                    if ((travelDesk.CreatedBy) != null)
                    {
                        spCreatedby.Value = travelDesk.CreatedBy;
                        spCreatedby.SqlDbType = SqlDbType.NVarChar;
                    }
                    else
                    {
                        spCreatedby.Value = null;
                        spCreatedby.SqlDbType = SqlDbType.NVarChar;
                    }
                    paramColl.Add(spCreatedby);

                    SqlParameter spticketbookdate = new SqlParameter();
                    spticketbookdate.ParameterName = "@CreatedDate";

                    spticketbookdate.Value = DateTime.UtcNow.AddHours(5.5).ToString();
                    spticketbookdate.SqlDbType = SqlDbType.DateTime;

                    paramColl.Add(spticketbookdate);

                    GDSDBWrapper.ExecuteNonQueryOutParam("sp_CreateTravelDesk", paramColl);
                    result = int.Parse(errorCount.Value.ToString());
                }
                else
                {
                    SqlParameter spId = new SqlParameter();
                    spId.ParameterName = "@Id";
                    if ((travelDesk.Id) != null)
                    {
                        spId.Value = travelDesk.Id;
                        spId.SqlDbType = SqlDbType.Int;
                    }
                    else
                    {
                        spId.Value = null;
                        spId.SqlDbType = SqlDbType.Int;
                    }
                    paramColl.Add(spId);



                    GDSDBWrapper.ExecuteNonQueryOutParam("sp_UpdateTravelDesk", paramColl);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }

        public int? CreateNewZone(Zones zones)
        {
            ArrayList paramColl = null;
            int? result = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter spgroupId = new SqlParameter();
                spgroupId.ParameterName = "@Zone";
                if ((zones.Zone) != null)
                {
                    spgroupId.Value = zones.Zone;
                    spgroupId.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spgroupId.Value = null;
                    spgroupId.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spgroupId);

                SqlParameter spgroupDesc = new SqlParameter();
                spgroupDesc.ParameterName = "@Description";
                if ((zones.Description) != null)
                {
                    spgroupDesc.Value = zones.Description;
                    spgroupDesc.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spgroupDesc.Value = null;
                    spgroupDesc.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spgroupDesc);
                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramColl.Add(errorCount);

                if (zones.Id != null)
                {
                    SqlParameter spzoneid = new SqlParameter();
                    spzoneid.ParameterName = "@Id";
                    spzoneid.Value = zones.Id;
                    spzoneid.SqlDbType = SqlDbType.Int;
                    paramColl.Add(spzoneid);

                    SqlParameter spUpdateBy = new SqlParameter();
                    spUpdateBy.ParameterName = "@ModifiedBy";
                    spUpdateBy.Value = zones.ModifiedBy;
                    spUpdateBy.SqlDbType = SqlDbType.NVarChar;
                    paramColl.Add(spUpdateBy);
                    GDSDBWrapper.ExecuteNonQueryOutParam("sp_UpdateZone", paramColl);
                }
                else
                {
                    SqlParameter spCreatedBy = new SqlParameter();
                    spCreatedBy.ParameterName = "@CreatedBy";
                    spCreatedBy.Value = zones.CreatedBy;
                    spCreatedBy.SqlDbType = SqlDbType.NVarChar;
                    paramColl.Add(spCreatedBy);
                    GDSDBWrapper.ExecuteNonQueryOutParam("sp_CreateNewZone", paramColl);
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }

        public int? CreateLeave(Leave Leave)
        {

            ArrayList paramColl = null;
            int? result = null;
            try
            {
                paramColl = new ArrayList();


                SqlParameter employeeid = new SqlParameter();
                employeeid.ParameterName = "@Employee_ID";
                if ((Leave.Employee_ID) != null)
                {
                    employeeid.Value = Leave.Employee_ID;
                    employeeid.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    employeeid.Value = null;
                    employeeid.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(employeeid);


                SqlParameter leavefrom = new SqlParameter();
                leavefrom.ParameterName = "@LeaveFrom";
                if ((Leave.LeaveFrom) != null)
                {
                    leavefrom.Value = Leave.LeaveFrom;
                    leavefrom.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    leavefrom.Value = null;
                    leavefrom.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(leavefrom);

                SqlParameter leaveto = new SqlParameter();
                leaveto.ParameterName = "@LeaveTo";
                if ((Leave.LeaveTo) != null)
                {
                    leaveto.Value = Leave.LeaveTo;
                    leaveto.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    leaveto.Value = Leave.LeaveFrom; ;
                    leaveto.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(leaveto);


                SqlParameter leavetype = new SqlParameter();
                leavetype.ParameterName = "@LeaveType";
                if ((Leave.LeaveType) != null)
                {
                    leavetype.Value = Leave.LeaveType;
                    leavetype.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    leavetype.Value = null;
                    leavetype.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(leavetype);


                SqlParameter leavereason = new SqlParameter();
                leavereason.ParameterName = "@LeaveComment";
                if ((Leave.LeaveComment) != null)
                {
                    leavereason.Value = Leave.LeaveComment;
                    leavereason.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    leavereason.Value = null;
                    leavereason.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(leavereason);



                SqlParameter leavecreated = new SqlParameter();
                leavecreated.ParameterName = "@CreatedDate";

                leavecreated.Value = DateTime.UtcNow.AddHours(5.5).ToString();
                leavecreated.SqlDbType = SqlDbType.DateTime;

                paramColl.Add(leavecreated);

                SqlParameter leavestatus = new SqlParameter();
                leavestatus.ParameterName = "@LeaveStatus";
                if ((Leave.LeaveStatus) != null)
                {
                    leavestatus.Value = Leave.LeaveStatus;
                    leavestatus.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    leavestatus.Value = null;
                    leavestatus.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(leavestatus);


                SqlParameter leavedays = new SqlParameter();
                leavedays.ParameterName = "@NoOfDays";
                if ((Leave.NoofDays) != null)
                {
                    leavedays.Value = Leave.NoofDays;
                    leavedays.SqlDbType = SqlDbType.Int;

                }
                else
                {

                    leavedays.Value = null;
                    leavedays.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(leavedays);


                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramColl.Add(errorCount);

                GDSDBWrapper.ExecuteNonQueryOutParam("sp_CreateLeave", paramColl);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }

        public DataTable GetZonesList()
        {
            DataTable counttable = new DataTable();
            ArrayList paramRequest = new ArrayList();
            try
            {
                counttable = GDSDBWrapper.ExecuteDataReaderMultiParam("[sp_GetZonesList]", paramRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return counttable;


        }
        public DataTable GetEventsList()
        {
            DataTable counttable = new DataTable();
            ArrayList paramRequest = new ArrayList();
            try
            {
                counttable = GDSDBWrapper.ExecuteDataReaderMultiParam("sp_GetEventsList", paramRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return counttable;


        }
        public DataTable GetNewsList()
        {
            DataTable counttable = new DataTable();
            ArrayList paramRequest = new ArrayList();
            try
            {
                counttable = GDSDBWrapper.ExecuteDataReaderMultiParam("sp_GetNewsList", paramRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return counttable;


        }
        public DataTable GetUserGroupList()
        {
            DataTable counttable = new DataTable();
            ArrayList paramRequest = new ArrayList();
            try
            {
                counttable = GDSDBWrapper.ExecuteDataReaderMultiParam("[SP_GetUserGroups]", paramRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return counttable;


        }

        public DataTable GetZones()
        {
            DataTable dtZonesData = new DataTable();

            try
            {
                ArrayList zones = new ArrayList();

                dtZonesData = GDSDBWrapper.ExecuteDataReaderMultiParam("sp_GetZonesList", zones);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return dtZonesData;
        }

        public DataTable GetStates()
        {
            DataTable dtStates = new DataTable();

            try
            {
                ArrayList states = new ArrayList();

                dtStates = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetStates", states);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return dtStates;


        }

        public DataTable GetCities()
        {
            DataTable dtCities = new DataTable();

            try
            {
                ArrayList cities = new ArrayList();

                dtCities = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetCities", cities);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtCities;
        }

        public DataTable GetUserGroups()
        {
            DataTable dtUserGroups = new DataTable();

            try
            {
                ArrayList users = new ArrayList();

                dtUserGroups = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetUserGroups", users);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtUserGroups;
        }


        public DataTable IsAlreadyApplied(string emailId,int? jobid)
        {
            DataTable dt = new DataTable();

            try
            {
                ArrayList paramColl = null;
                paramColl = new ArrayList();

                SqlParameter spgroupId = new SqlParameter();
                spgroupId.ParameterName = "@EmailId";
                spgroupId.Value = emailId;
                spgroupId.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(spgroupId);


                SqlParameter spjobid = new SqlParameter();
                spjobid.ParameterName = "@JobId";
                spjobid.Value = jobid;
                spjobid.SqlDbType = SqlDbType.Int;
                paramColl.Add(spjobid);

                dt = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_IsAlreadyApplied", paramColl);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dt;
        }



        public int? CreateEmployee(Employees emp, int flag)
        {
            ArrayList paramColl = null;
            int? result = null;
            try
            {
                paramColl = new ArrayList();

                #region parameters

                #region employee id
                SqlParameter spEmployeeId = new SqlParameter();
                spEmployeeId.ParameterName = "@EmployeeID";
                if ((emp.Employee_ID) != null)
                {
                    spEmployeeId.Value = emp.Employee_ID;
                    spEmployeeId.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spEmployeeId.Value = null;
                    spEmployeeId.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spEmployeeId);
                #endregion

                #region firstName
                SqlParameter spFirstName = new SqlParameter();
                spFirstName.ParameterName = "@FirstName";
                if ((emp.FirstName) != null)
                {
                    spFirstName.Value = emp.FirstName;
                    spFirstName.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spFirstName.Value = null;
                    spFirstName.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spFirstName);
                #endregion

                #region MiddleName
                SqlParameter spMiddleName = new SqlParameter();
                spMiddleName.ParameterName = "@MiddleName";
                if ((emp.MiddleName) != null)
                {
                    spMiddleName.Value = emp.MiddleName;
                    spMiddleName.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spMiddleName.Value = null;
                    spMiddleName.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spMiddleName);
                #endregion

                #region LastName
                SqlParameter spLastName = new SqlParameter();
                spLastName.ParameterName = "@LastName";
                if ((emp.LastName) != null)
                {
                    spLastName.Value = emp.LastName;
                    spLastName.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spLastName.Value = null;
                    spLastName.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spLastName);
                #endregion

                #region FathersName
                SqlParameter spFathersName = new SqlParameter();
                spFathersName.ParameterName = "@FathersName";
                if ((emp.FathersName) != null)
                {
                    spFathersName.Value = emp.FathersName;
                    spFathersName.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spFathersName.Value = null;
                    spFathersName.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spFathersName);
                #endregion

                #region MotherName
                SqlParameter spMothersName = new SqlParameter();
                spMothersName.ParameterName = "@MothersName";
                if ((emp.MothersName) != null)
                {
                    spMothersName.Value = emp.MothersName;
                    spMothersName.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spMothersName.Value = null;
                    spMothersName.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spMothersName);
                #endregion

                #region date of birth
                SqlParameter spDOB = new SqlParameter();
                spDOB.ParameterName = "@DOB";
                if ((emp.DOB) != null)
                {
                    spDOB.Value = emp.DOB;
                    spDOB.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spDOB.Value = null;
                    spDOB.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spDOB);
                #endregion

                #region gender
                SqlParameter spGender = new SqlParameter();
                spGender.ParameterName = "@Gender";
                if ((emp.Gender) != null)
                {
                    spGender.Value = emp.Gender;
                    spGender.SqlDbType = SqlDbType.Char;
                }
                else
                {
                    spGender.Value = null;
                    spGender.SqlDbType = SqlDbType.Char;
                }
                paramColl.Add(spGender);
                #endregion

                #region zone
                SqlParameter spZone = new SqlParameter();
                spZone.ParameterName = "@FKLocationorZone";
                if ((emp.FK_LocationorZone) != null)
                {
                    spZone.Value = emp.FK_LocationorZone;
                    spZone.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spZone.Value = null;
                    spZone.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spZone);
                #endregion

                #region Manager id
                SqlParameter spManagerName = new SqlParameter();
                spManagerName.ParameterName = "@ManagerId";
                if ((emp.ManagerId) != null)
                {
                    spManagerName.Value = emp.ManagerId;
                    spManagerName.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spManagerName.Value = null;
                    spManagerName.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spManagerName);
                #endregion



                #region Account Manager id
                SqlParameter spAccountManagerName = new SqlParameter();
                spAccountManagerName.ParameterName = "@AccountManagerId";
                if ((emp.AccountManagerId) != null)
                {
                    spAccountManagerName.Value = emp.AccountManagerId;
                    spAccountManagerName.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spAccountManagerName.Value = null;
                    spAccountManagerName.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spAccountManagerName);
                #endregion


                #region driving licence expiry date

                SqlParameter DrivingLicenceExpiryDate = new SqlParameter();
                DrivingLicenceExpiryDate.ParameterName = "@DrivingLicenceExpiryDate";
                if ((emp.DrivingLicenceExpiryDate) != null)
                {
                    DrivingLicenceExpiryDate.Value = emp.DrivingLicenceExpiryDate;
                    DrivingLicenceExpiryDate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    DrivingLicenceExpiryDate.Value = null;
                    DrivingLicenceExpiryDate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(DrivingLicenceExpiryDate);

                #endregion
                #region pan no
                SqlParameter spPANNo = new SqlParameter();
                spPANNo.ParameterName = "@PANNo";
                if ((emp.PANNo) != null)
                {
                    spPANNo.Value = emp.PANNo;
                    spPANNo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spPANNo.Value = null;
                    spPANNo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spPANNo);
                #endregion

                #region aadhar no

                SqlParameter spAadharNo = new SqlParameter();
                spAadharNo.ParameterName = "@AadharNo";
                if ((emp.AadharNo) != null)
                {
                    spAadharNo.Value = emp.AadharNo;
                    spAadharNo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spAadharNo.Value = null;
                    spAadharNo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spAadharNo);

                #endregion

                #region driving licence no

                SqlParameter spDrivingLicenceNo = new SqlParameter();
                spDrivingLicenceNo.ParameterName = "@DrivingLicenceNo";
                if ((emp.DrivingLicenceNo) != null)
                {
                    spDrivingLicenceNo.Value = emp.DrivingLicenceNo;
                    spDrivingLicenceNo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spDrivingLicenceNo.Value = null;
                    spDrivingLicenceNo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spDrivingLicenceNo);

                #endregion

                #region SAP business id

                SqlParameter spSAPBussinessId = new SqlParameter();
                spSAPBussinessId.ParameterName = "@SAPBussinessId";
                if ((emp.SAPBussinessId) != null)
                {
                    spSAPBussinessId.Value = emp.SAPBussinessId;
                    spSAPBussinessId.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spSAPBussinessId.Value = null;
                    spSAPBussinessId.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spSAPBussinessId);

                #endregion

                #region PF No

                SqlParameter spPFNo = new SqlParameter();
                spPFNo.ParameterName = "@PFNo";
                if ((emp.PFNo) != null)
                {
                    spPFNo.Value = emp.PFNo;
                    spPFNo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spPFNo.Value = null;
                    spPFNo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spPFNo);

                #endregion

                #region UAN for PF no

                SqlParameter spUANforPFNo = new SqlParameter();
                spUANforPFNo.ParameterName = "@UANforPFNo";
                if ((emp.UANforPFNo) != null)
                {
                    spUANforPFNo.Value = emp.UANforPFNo;
                    spUANforPFNo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spUANforPFNo.Value = null;
                    spUANforPFNo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spUANforPFNo);

                #endregion

                #region ESI No

                SqlParameter spESINo = new SqlParameter();
                spESINo.ParameterName = "@ESINo";
                if ((emp.ESINo) != null)
                {
                    spESINo.Value = emp.ESINo;
                    spESINo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spESINo.Value = null;
                    spESINo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spESINo);

                #endregion

                #region Blood Group

                SqlParameter spBloodGroup = new SqlParameter();
                spBloodGroup.ParameterName = "@BloodGroup";
                if ((emp.BloodGroup) != null)
                {
                    spBloodGroup.Value = emp.BloodGroup;
                    spBloodGroup.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spBloodGroup.Value = null;
                    spBloodGroup.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spBloodGroup);

                #endregion

                #region Mobile No

                SqlParameter spMobileNo = new SqlParameter();
                spMobileNo.ParameterName = "@MobileNo";
                if ((emp.MobileNo) != null)
                {
                    spMobileNo.Value = emp.MobileNo;
                    spMobileNo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spMobileNo.Value = null;
                    spMobileNo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spMobileNo);

                #endregion

                #region Emergence contact names

                SqlParameter spEmergencyContactPerson = new SqlParameter();
                spEmergencyContactPerson.ParameterName = "@EmergencyContactPerson";
                if ((emp.EmergencyContactPerson) != null)
                {
                    spEmergencyContactPerson.Value = emp.EmergencyContactPerson;
                    spEmergencyContactPerson.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spEmergencyContactPerson.Value = null;
                    spEmergencyContactPerson.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spEmergencyContactPerson);

                #endregion

                #region emergence contact no

                SqlParameter spEmergencyContactNo = new SqlParameter();
                spEmergencyContactNo.ParameterName = "@EmergencyContactNo";
                if ((emp.EmergencyContactNo) != null)
                {
                    spEmergencyContactNo.Value = emp.EmergencyContactNo;
                    spEmergencyContactNo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spEmergencyContactNo.Value = null;
                    spEmergencyContactNo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spEmergencyContactNo);

                #endregion

                #region date of joining

                SqlParameter spDOJ = new SqlParameter();
                spDOJ.ParameterName = "@DOJ";
                if ((emp.DOJ) != null)
                {
                    spDOJ.Value = emp.DOJ;
                    spDOJ.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spDOJ.Value = null;
                    spDOJ.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spDOJ);

                #endregion

                #region personal mail id

                SqlParameter spPersonalEmailID = new SqlParameter();
                spPersonalEmailID.ParameterName = "@PersonalEmailID";
                if ((emp.PersonalEmailID) != null)
                {
                    spPersonalEmailID.Value = emp.PersonalEmailID;
                    spPersonalEmailID.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spPersonalEmailID.Value = null;
                    spPersonalEmailID.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spPersonalEmailID);

                #endregion

                #region company mail id

                SqlParameter spCompanyMailID = new SqlParameter();
                spCompanyMailID.ParameterName = "@CompanyMailID";
                if ((emp.CompanyMailID) != null)
                {
                    spCompanyMailID.Value = emp.CompanyMailID;
                    spCompanyMailID.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spCompanyMailID.Value = null;
                    spCompanyMailID.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spCompanyMailID);

                #endregion

                #region user group

                SqlParameter spUserGroup = new SqlParameter();
                spUserGroup.ParameterName = "@UserGroup";
                if ((emp.FK_UserGroup) != null)
                {
                    spUserGroup.Value = emp.FK_UserGroup;
                    spUserGroup.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spUserGroup.Value = null;
                    spUserGroup.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spUserGroup);

                #endregion

                #region vehicle type

                SqlParameter spVehicleType = new SqlParameter();
                spVehicleType.ParameterName = "@VehicleType";
                if ((emp.VehicleType) != null)
                {
                    spVehicleType.Value = emp.VehicleType;
                    spVehicleType.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spVehicleType.Value = null;
                    spVehicleType.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spVehicleType);

                #endregion

                #region conveyance

                SqlParameter spConveyance = new SqlParameter();
                spConveyance.ParameterName = "@Conveyance";
                if ((emp.Conveyance) != null)
                {
                    spConveyance.Value = emp.Conveyance;
                    spConveyance.SqlDbType = SqlDbType.Float;
                }
                else
                {
                    spConveyance.Value = null;
                    spConveyance.SqlDbType = SqlDbType.Float;
                }
                paramColl.Add(spConveyance);

                #endregion

                #region LumpsumAmount

                SqlParameter LumpsumAmount = new SqlParameter();
                LumpsumAmount.ParameterName = "@LumpsumAmount";
                if ((emp.LumpsumAmount) != null)
                {
                    LumpsumAmount.Value = emp.LumpsumAmount;
                    LumpsumAmount.SqlDbType = SqlDbType.Float;
                }
                else
                {
                    LumpsumAmount.Value = null;
                    LumpsumAmount.SqlDbType = SqlDbType.Float;
                }
                paramColl.Add(LumpsumAmount);

                #endregion

                #region passport

                SqlParameter spPassport = new SqlParameter();
                spPassport.ParameterName = "@PassportNo";
                if ((emp.PassportNumber) != null)
                {
                    spPassport.Value = emp.PassportNumber;
                    spPassport.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spPassport.Value = null;
                    spPassport.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spPassport);

                #endregion

                #region passport validate

                SqlParameter spPassportValidate = new SqlParameter();
                spPassportValidate.ParameterName = "@PassportValidate";
                if ((emp.PassportValidateDate) != null)
                {
                    spPassportValidate.Value = emp.PassportValidateDate;
                    spPassportValidate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spPassportValidate.Value = null;
                    spPassportValidate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spPassportValidate);
                #endregion

                #region permanent address

                SqlParameter spPermanentAddress = new SqlParameter();
                spPermanentAddress.ParameterName = "@PermanentAddress";
                if ((emp.PermanentAddress) != null)
                {
                    spPermanentAddress.Value = emp.PermanentAddress;
                    spPermanentAddress.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spPermanentAddress.Value = null;
                    spPermanentAddress.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spPermanentAddress);

                #endregion

                #region permanent state

                SqlParameter spPermanentState = new SqlParameter();
                spPermanentState.ParameterName = "@PermanentState";
                if ((emp.FK_PermanentState) != null)
                {
                    spPermanentState.Value = emp.FK_PermanentState;
                    spPermanentState.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spPermanentState.Value = null;
                    spPermanentState.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spPermanentState);

                #endregion

                #region permanent District

                SqlParameter spPermanentCity = new SqlParameter();
                spPermanentCity.ParameterName = "@PermanentCity";
                if ((emp.FK_PermanentCity) != null)
                {
                    spPermanentCity.Value = emp.FK_PermanentCity;
                    spPermanentCity.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spPermanentCity.Value = null;
                    spPermanentCity.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spPermanentCity);

                #endregion

                #region permanent pincode

                SqlParameter spPermanentPincode = new SqlParameter();
                spPermanentPincode.ParameterName = "@PermanentPincode";
                if ((emp.PermanentPincode) != null)
                {
                    spPermanentPincode.Value = emp.PermanentPincode;
                    spPermanentPincode.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spPermanentPincode.Value = null;
                    spPermanentPincode.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spPermanentPincode);

                #endregion

                #region temp address

                SqlParameter spTempAddress = new SqlParameter();
                spTempAddress.ParameterName = "@TempAddress";
                if ((emp.TempAddress) != null)
                {
                    spTempAddress.Value = emp.TempAddress;
                    spTempAddress.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spTempAddress.Value = null;
                    spTempAddress.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spTempAddress);

                #endregion

                #region temp state

                SqlParameter spTempState = new SqlParameter();
                spTempState.ParameterName = "@TempState";
                if ((emp.FK_TempState) != null)
                {
                    spTempState.Value = emp.FK_TempState;
                    spTempState.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spTempState.Value = null;
                    spTempState.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spTempState);

                #endregion

                #region temp city

                SqlParameter spTempCity = new SqlParameter();
                spTempCity.ParameterName = "@TempCity";
                if ((emp.FK_TempCity) != null)
                {
                    spTempCity.Value = emp.FK_TempCity;
                    spTempCity.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spTempCity.Value = null;
                    spTempCity.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spTempCity);

                #endregion

                #region temp pincode

                SqlParameter spTempPincode = new SqlParameter();
                spTempPincode.ParameterName = "@TempPincode";
                if ((emp.TempPincode) != null)
                {
                    spTempPincode.Value = emp.TempPincode;
                    spTempPincode.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spTempPincode.Value = null;
                    spTempPincode.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spTempPincode);

                #endregion

                #region bank name

                SqlParameter spBankName = new SqlParameter();
                spBankName.ParameterName = "@BankName";
                if ((emp.BankName) != null)
                {
                    spBankName.Value = emp.BankName;
                    spBankName.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spBankName.Value = null;
                    spBankName.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spBankName);

                #endregion

                #region branch

                SqlParameter spBranchandLocation = new SqlParameter();
                spBranchandLocation.ParameterName = "@BranchandLocation";
                if ((emp.BranchandLocation) != null)
                {
                    spBranchandLocation.Value = emp.BranchandLocation;
                    spBranchandLocation.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spBranchandLocation.Value = null;
                    spBranchandLocation.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spBranchandLocation);

                #endregion

                #region account no

                SqlParameter spBankAccountNo = new SqlParameter();
                spBankAccountNo.ParameterName = "@BankAccountNo";
                if ((emp.BankAccountNo) != null)
                {
                    spBankAccountNo.Value = emp.BankAccountNo;
                    spBankAccountNo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spBankAccountNo.Value = null;
                    spBankAccountNo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spBankAccountNo);

                #endregion

                #region IFSC code

                SqlParameter spBankIFSCCode = new SqlParameter();
                spBankIFSCCode.ParameterName = "@BankIFSCCode";
                if ((emp.BankIFSCCode) != null)
                {
                    spBankIFSCCode.Value = emp.BankIFSCCode;
                    spBankIFSCCode.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spBankIFSCCode.Value = null;
                    spBankIFSCCode.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spBankIFSCCode);

                #endregion

                #region NoOfLeaves

                SqlParameter NoOfLeaves = new SqlParameter();
                NoOfLeaves.ParameterName = "@NoOfLeaves";
                if ((emp.NoOfLeaves) != null)
                {
                    NoOfLeaves.Value = emp.NoOfLeaves;
                    NoOfLeaves.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    NoOfLeaves.Value = null;
                    NoOfLeaves.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(NoOfLeaves);

                #endregion

                #region Profile Image

                SqlParameter profile = new SqlParameter();
                profile.ParameterName = "@ProfilePicturePath";
                if ((emp.Temimagepath) != null)
                {
                    profile.Value = emp.Temimagepath;
                    profile.SqlDbType = SqlDbType.VarChar;
                }
                //else if(emp.ProfilePicturePath
                else
                {
                    profile.Value = null;
                    profile.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(profile);

                #endregion


                #region MaritalStatus

                SqlParameter MaritalStatus = new SqlParameter();
                MaritalStatus.ParameterName = "@MaritalStatus";
                if ((emp.MaritalStatus) != null)
                {
                    MaritalStatus.Value = emp.MaritalStatus;
                    MaritalStatus.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    MaritalStatus.Value = null;
                    MaritalStatus.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(MaritalStatus);

                #endregion

                #region PermanentCityname
                SqlParameter PermanentCityname = new SqlParameter();
                PermanentCityname.ParameterName = "@PermanentCityname";
                if ((emp.PermanentCityname) != null)
                {
                    PermanentCityname.Value = emp.PermanentCityname;
                    PermanentCityname.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    PermanentCityname.Value = null;
                    PermanentCityname.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(PermanentCityname);
                #endregion

                #region CurrentCityname
                SqlParameter CurrentCityname = new SqlParameter();
                CurrentCityname.ParameterName = "@CurrentCityname";
                if ((emp.CurrentCityname) != null)
                {
                    CurrentCityname.Value = emp.CurrentCityname;
                    CurrentCityname.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    CurrentCityname.Value = null;
                    CurrentCityname.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(CurrentCityname);
                #endregion


                #region NOK

                SqlParameter NOK = new SqlParameter();
                NOK.ParameterName = "@NOK";
                if ((emp.NOK) != null)
                {
                    NOK.Value = emp.NOK;
                    NOK.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    NOK.Value = null;
                    NOK.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(NOK);

                #endregion

                #endregion

                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramColl.Add(errorCount);



                if (flag == 1)
                {
                    SqlParameter spCreatedBy = new SqlParameter();
                    spCreatedBy.ParameterName = "@CreatedBy";
                    spCreatedBy.Value = emp.CreatedBy;
                    spCreatedBy.SqlDbType = SqlDbType.NVarChar;
                    paramColl.Add(spCreatedBy);

                    GDSDBWrapper.ExecuteNonQueryOutParam("SP_CreateEmployee", paramColl);
                }
                else
                {
                    SqlParameter spId = new SqlParameter();
                    spId.ParameterName = "@Id";
                    spId.Value = emp.Id;
                    spId.SqlDbType = SqlDbType.Int;
                    paramColl.Add(spId);

                    SqlParameter spStatus = new SqlParameter();
                    spStatus.ParameterName = "@Status";
                    if ((emp.Status) != null)
                    {
                        spStatus.Value = emp.Status;
                        spStatus.SqlDbType = SqlDbType.Bit;
                    }
                    else
                    {
                        spStatus.Value = null;
                        spStatus.SqlDbType = SqlDbType.Bit;
                    }
                    paramColl.Add(spStatus);


                    SqlParameter sppassword = new SqlParameter();
                    sppassword.ParameterName = "@Password";
                    if ((emp.Password) != null)
                    {
                        sppassword.Value = emp.Password;
                        sppassword.SqlDbType = SqlDbType.NVarChar;
                    }
                    else
                    {
                        sppassword.Value = null;
                        sppassword.SqlDbType = SqlDbType.NVarChar;
                    }
                    paramColl.Add(sppassword);

                    SqlParameter spCompanyLeftDate = new SqlParameter();
                    spCompanyLeftDate.ParameterName = "@CompanyLeftDate";
                    if ((emp.CompanyLeftDate) != null)
                    {
                        spCompanyLeftDate.Value = emp.CompanyLeftDate;
                        spCompanyLeftDate.SqlDbType = SqlDbType.DateTime;
                    }
                    else
                    {
                        spCompanyLeftDate.Value = null;
                        spCompanyLeftDate.SqlDbType = SqlDbType.DateTime;
                    }
                    paramColl.Add(spCompanyLeftDate);

                    SqlParameter spLeavingReason = new SqlParameter();
                    spLeavingReason.ParameterName = "@LeavingReason";
                    if ((emp.LeavingReason) != null)
                    {
                        spLeavingReason.Value = emp.LeavingReason;
                        spLeavingReason.SqlDbType = SqlDbType.NVarChar;
                    }
                    else
                    {
                        spLeavingReason.Value = null;
                        spLeavingReason.SqlDbType = SqlDbType.NVarChar;
                    }
                    paramColl.Add(spLeavingReason);

                    SqlParameter spUpdateBy = new SqlParameter();
                    spUpdateBy.ParameterName = "@ModifiedBy";
                    spUpdateBy.Value = emp.ModifiedBy;
                    spUpdateBy.SqlDbType = SqlDbType.NVarChar;
                    paramColl.Add(spUpdateBy);
                    GDSDBWrapper.ExecuteNonQueryOutParam("SP_UpdateEmployee", paramColl);
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }

        public int? ApplyToJob(Jobs_AppliedCandidates jobs_AppliedCandidates)
        {
            ArrayList paramColl = null;
            int? result = null;
            try
            {
                paramColl = new ArrayList();

                #region parameters

                SqlParameter spJobId = new SqlParameter();
                spJobId.ParameterName = "@JobId";
                if ((jobs_AppliedCandidates.JobId) != null)
                {
                    spJobId.Value = jobs_AppliedCandidates.JobId;
                    spJobId.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spJobId.Value = null;
                    spJobId.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spJobId);

                #region Name
                SqlParameter spFirstName = new SqlParameter();
                spFirstName.ParameterName = "@Name";
                if ((jobs_AppliedCandidates.Name) != null)
                {
                    spFirstName.Value = jobs_AppliedCandidates.Name;
                    spFirstName.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spFirstName.Value = null;
                    spFirstName.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spFirstName);
                #endregion


                SqlParameter spEmailId = new SqlParameter();
                spEmailId.ParameterName = "@EmailId";
                if ((jobs_AppliedCandidates.EmailId) != null)
                {
                    spEmailId.Value = jobs_AppliedCandidates.EmailId;
                    spEmailId.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spEmailId.Value = null;
                    spEmailId.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spEmailId);

                SqlParameter spPhone = new SqlParameter();
                spPhone.ParameterName = "@Phone";
                if ((jobs_AppliedCandidates.Phone) != null)
                {
                    spPhone.Value = jobs_AppliedCandidates.Phone;
                    spPhone.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spPhone.Value = null;
                    spPhone.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spPhone);


                SqlParameter spAddress = new SqlParameter();
                spAddress.ParameterName = "@Address";
                if ((jobs_AppliedCandidates.Address) != null)
                {
                    spAddress.Value = jobs_AppliedCandidates.Address;
                    spAddress.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spAddress.Value = null;
                    spAddress.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spAddress);


                SqlParameter spAvailability = new SqlParameter();
                spAvailability.ParameterName = "@Availability";
                if ((jobs_AppliedCandidates.Availability) != null)
                {
                    spAvailability.Value = jobs_AppliedCandidates.Availability;
                    spAvailability.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spAvailability.Value = null;
                    spAvailability.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spAvailability);

                SqlParameter spPastExperience = new SqlParameter();
                spPastExperience.ParameterName = "@PastExperience";
                if ((jobs_AppliedCandidates.PastExperience) != null)
                {
                    spPastExperience.Value = jobs_AppliedCandidates.PastExperience;
                    spPastExperience.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spPastExperience.Value = null;
                    spPastExperience.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spPastExperience);

                SqlParameter spTrainingRequired = new SqlParameter();
                spTrainingRequired.ParameterName = "@TrainingRequired";
                if ((jobs_AppliedCandidates.TrainingRequired) != null)
                {
                    spTrainingRequired.Value = jobs_AppliedCandidates.TrainingRequired;
                    spTrainingRequired.SqlDbType = SqlDbType.Bit;
                }
                else
                {
                    spTrainingRequired.Value = null;
                    spTrainingRequired.SqlDbType = SqlDbType.Bit;
                }
                paramColl.Add(spTrainingRequired);

                SqlParameter spResumeFileName = new SqlParameter();
                spResumeFileName.ParameterName = "@ResumeFileName";
                if ((jobs_AppliedCandidates.ResumeFileName) != null)
                {
                    spResumeFileName.Value = jobs_AppliedCandidates.ResumeFileName;
                    spResumeFileName.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spResumeFileName.Value = null;
                    spResumeFileName.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spResumeFileName);

                #endregion

                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramColl.Add(errorCount);


                GDSDBWrapper.ExecuteNonQueryOutParam("SP_ApplyJob", paramColl);
            }



            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }

        public DataSet GetZone(int? id)
        {
            ArrayList paramColl = null;
            DataSet dt = new DataSet();
            try
            {
                paramColl = new ArrayList();

                SqlParameter spgroupId = new SqlParameter();
                spgroupId.ParameterName = "@Id";
                if (id != null)
                {
                    spgroupId.Value = id;
                    spgroupId.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spgroupId.Value = null;
                    spgroupId.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spgroupId);

                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramColl.Add(errorCount);

                dt = GDSDBWrapper.ExecuteCustomDatasetMultiParam("sp_GetZone", paramColl);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return dt;
        }

        public DataSet GetAccountManagerDetails()
        {
            DataSet dtAccountManager = new DataSet();

            try
            {
                ArrayList emp = new ArrayList();

                dtAccountManager = GDSDBWrapper.ExecuteCustomDatasetMultiParam("SP_GetAccontManagerDetails", emp);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return dtAccountManager;
        }

        public DataTable GetManagerDetails(int? ZoneId)
        {
            DataTable dtManagerDetails = new DataTable();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter spgroupId = new SqlParameter();
                spgroupId.ParameterName = "@ZoneId";
                if (ZoneId != null)
                {
                    spgroupId.Value = ZoneId;
                    spgroupId.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spgroupId.Value = null;
                    spgroupId.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spgroupId);

                dtManagerDetails = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetManagerDetails", paramColl);
            }
            catch (Exception Ex)
            {

                throw;
            }

            return dtManagerDetails;
        }

        public DataSet GetEmployeeByID(string userid, int? Id)
        {
            ArrayList paramColl = null;
            DataSet dt = new DataSet();
            try
            {
                paramColl = new ArrayList();

                if (userid != null)
                {
                    SqlParameter spId = new SqlParameter();
                    spId.ParameterName = "@userid";
                    if (userid != null)
                    {
                        spId.Value = userid;
                        spId.SqlDbType = SqlDbType.NVarChar;
                    }
                    else
                    {
                        spId.Value = null;
                        spId.SqlDbType = SqlDbType.NVarChar;
                    }
                    paramColl.Add(spId);
                    dt = GDSDBWrapper.ExecuteCustomDatasetMultiParam("SP_GetEmployeeByUserID", paramColl);
                }
                else if (Id != null)
                {

                    SqlParameter spId = new SqlParameter();
                    spId.ParameterName = "@Id";
                    if (Id != null)
                    {
                        spId.Value = Id;
                        spId.SqlDbType = SqlDbType.Int;
                    }
                    else
                    {
                        spId.Value = null;
                        spId.SqlDbType = SqlDbType.Int;
                    }
                    paramColl.Add(spId);

                    dt = GDSDBWrapper.ExecuteCustomDatasetMultiParam("SP_GetEmployeeByID", paramColl);
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return dt;
        }

        public object CheckEmployeeID(string EmpID)
        {
            object EmployeeID = new object();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter spEmployeeID = new SqlParameter();
                spEmployeeID.ParameterName = "@EmpID";
                if (EmpID != null)
                {
                    spEmployeeID.Value = EmpID;
                    spEmployeeID.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spEmployeeID = null;
                    spEmployeeID.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spEmployeeID);

                EmployeeID = GDSDBWrapper.ExecuteScalar("SP_ValidateEmployeeID", paramColl);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return EmployeeID;
        }

        public object CheckAadharID(string AadharID)
        {
            object Aadhar = new object();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter spAadharID = new SqlParameter();
                spAadharID.ParameterName = "@AadharID";
                if (AadharID != null)
                {
                    spAadharID.Value = AadharID;
                    spAadharID.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spAadharID = null;
                    spAadharID.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spAadharID);

                Aadhar = GDSDBWrapper.ExecuteScalar("SP_ValidateAadharID", paramColl);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Aadhar;

        }
        public object VerifyPassportNumber(string passport)
        {
            object Aadhar = new object();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter sppassport = new SqlParameter();
                sppassport.ParameterName = "@PassportId";
                if (passport != null)
                {
                    sppassport.Value = passport;
                    sppassport.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    sppassport = null;
                    sppassport.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(sppassport);

                Aadhar = GDSDBWrapper.ExecuteScalar("SP_VerifyPassportNumber", paramColl);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Aadhar;

        }
        public object CheckPancard(string Pancard)
        {
            object Pancardid = new object();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter spPancardid = new SqlParameter();
                spPancardid.ParameterName = "@Pancard";
                if (Pancard != null)
                {
                    spPancardid.Value = Pancard;
                    spPancardid.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spPancardid = null;
                    spPancardid.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spPancardid);

                Pancardid = GDSDBWrapper.ExecuteScalar("SP_ValidatePancard", paramColl);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Pancardid;

        }

        public object VerifyZoneName(string ZoneName)
        {
            object zone = new object();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter sppassport = new SqlParameter();
                sppassport.ParameterName = "@ZoneName";
                if (ZoneName != null)
                {
                    sppassport.Value = ZoneName;
                    sppassport.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    sppassport = null;
                    sppassport.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(sppassport);

                zone = GDSDBWrapper.ExecuteScalar("SP_VerifyZoneName", paramColl);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return zone;

        }

        public object VerifyExpenseType(string expensename)
        {
            object expense = new object();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter spexpense = new SqlParameter();
                spexpense.ParameterName = "@ExpanseName";
                if (expensename != null)
                {
                    spexpense.Value = expensename;
                    spexpense.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spexpense = null;
                    spexpense.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spexpense);

                expense = GDSDBWrapper.ExecuteScalar("SP_VerifyExpenseName", paramColl);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return expense;
        }

        public object VerifyUserGroupID(string UserGroupID)
        {
            object usergroup = new object();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter spexpense = new SqlParameter();
                spexpense.ParameterName = "@UserGroup_ID";
                if (UserGroupID != null)
                {
                    spexpense.Value = UserGroupID;
                    spexpense.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spexpense = null;
                    spexpense.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spexpense);

                usergroup = GDSDBWrapper.ExecuteScalar("SP_VerifyUserGroupID", paramColl);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return usergroup;
        }


        public object CheckDrivingLicenceNo(string DrivingLicence)
        {
            object drivingLicence = new object();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter spdrivingLicence = new SqlParameter();
                spdrivingLicence.ParameterName = "@DrivingLicence";
                if (DrivingLicence != null)
                {
                    spdrivingLicence.Value = DrivingLicence;
                    spdrivingLicence.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spdrivingLicence = null;
                    spdrivingLicence.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spdrivingLicence);

                drivingLicence = GDSDBWrapper.ExecuteScalar("SP_ValidateDrivingLicence", paramColl);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return drivingLicence;
        }
        public DataSet GetUserGroup(int? id)
        {
            ArrayList paramColl = null;
            DataSet dt = new DataSet();
            try
            {
                paramColl = new ArrayList();

                SqlParameter spgroupId = new SqlParameter();
                spgroupId.ParameterName = "@Id";
                if (id != null)
                {
                    spgroupId.Value = id;
                    spgroupId.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spgroupId.Value = null;
                    spgroupId.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spgroupId);



                dt = GDSDBWrapper.ExecuteCustomDatasetMultiParam("sp_GetUserGroup", paramColl);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return dt;
        }

        public object CheckExpenditureDateInLeave(DateTime ExpenditureDate, int? EmployeeID)
        {
            object Expendituredate = new object();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter spExpendituredate = new SqlParameter();
                spExpendituredate.ParameterName = "@ExpenditureDate";
                if (ExpenditureDate != null)
                {
                    spExpendituredate.Value = ExpenditureDate;
                    spExpendituredate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spExpendituredate = null;
                    spExpendituredate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spExpendituredate);


                SqlParameter spEmployeeID = new SqlParameter();
                spEmployeeID.ParameterName = "@EmployeeID";
                if (EmployeeID != null)
                {
                    spEmployeeID.Value = EmployeeID;
                    spEmployeeID.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spEmployeeID.Value = null;
                    spEmployeeID.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spEmployeeID);

                Expendituredate = GDSDBWrapper.ExecuteScalar("SP_ValidateExpenditureInLeave", paramColl);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Expendituredate;
        }

        public object CheckExpenditureDate(DateTime ExpenditureDate, int? EmployeeID)
        {
            object Expendituredate = new object();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter spExpendituredate = new SqlParameter();
                spExpendituredate.ParameterName = "@ExpenditureDate";
                if (ExpenditureDate != null)
                {
                    spExpendituredate.Value = ExpenditureDate;
                    spExpendituredate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spExpendituredate = null;
                    spExpendituredate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spExpendituredate);


                SqlParameter spEmployeeID = new SqlParameter();
                spEmployeeID.ParameterName = "@EmployeeID";
                if (EmployeeID != null)
                {
                    spEmployeeID.Value = EmployeeID;
                    spEmployeeID.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spEmployeeID.Value = null;
                    spEmployeeID.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spEmployeeID);


                Expendituredate = GDSDBWrapper.ExecuteScalar("SP_ValidateExpenditure", paramColl);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Expendituredate;
        }

        public int? CreateExpenditure(UserExpenses_Header ObjUserExpenseHeader, UserExpense_Details ObjeUserExpenseDetails, Employees emp, out int? detailID)
        {
            ArrayList paramColl = null;
            int? ExpenseHeaderID = null;

            try
            {
                paramColl = new ArrayList();
                #region parameters

                #region header

                #region employeeID
                SqlParameter spEmployeeId = new SqlParameter();
                spEmployeeId.ParameterName = "@EmployeeID";
                if (ObjUserExpenseHeader.FK_EmployeeId > 0)
                {
                    spEmployeeId.Value = ObjUserExpenseHeader.FK_EmployeeId;
                    spEmployeeId.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spEmployeeId.Value = null;
                    spEmployeeId.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spEmployeeId);
                #endregion

                #region userexpense type

                SqlParameter spuserExpenseType = new SqlParameter();
                spuserExpenseType.ParameterName = "@UserExpenseType";
                if (!string.IsNullOrEmpty(ObjUserExpenseHeader.UserExpenseType))
                {
                    spuserExpenseType.Value = ObjUserExpenseHeader.UserExpenseType;
                    spuserExpenseType.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spuserExpenseType.Value = null;
                    spuserExpenseType.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spuserExpenseType);

                #endregion

                #region Localization

                SqlParameter spLocalization = new SqlParameter();
                spLocalization.ParameterName = "@Localization";
                if (!string.IsNullOrEmpty(ObjUserExpenseHeader.Localization))
                {
                    spLocalization.Value = ObjUserExpenseHeader.Localization;
                    spLocalization.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spLocalization.Value = null;
                    spLocalization.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spLocalization);

                #endregion

                #region FromDate

                SqlParameter spFromDate = new SqlParameter();
                spFromDate.ParameterName = "@FromDate";
                if ((ObjUserExpenseHeader.FromDate) != null)
                {
                    spFromDate.Value = ObjUserExpenseHeader.FromDate;
                    spFromDate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spFromDate.Value = null;
                    spFromDate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spFromDate);

                #endregion

                #region ToDate

                SqlParameter spToDate = new SqlParameter();
                spToDate.ParameterName = "@ToDate";
                if ((ObjUserExpenseHeader.ToDate) != null)
                {
                    spToDate.Value = ObjUserExpenseHeader.ToDate;
                    spToDate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spToDate.Value = null;
                    spToDate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spToDate);

                #endregion

                #region ExpenseSheetName

                SqlParameter spExpenseSheetName = new SqlParameter();
                spExpenseSheetName.ParameterName = "@ExpenseSheetName";
                if (!string.IsNullOrEmpty(ObjUserExpenseHeader.ExpenseSheetName))
                {
                    spExpenseSheetName.Value = emp.FirstName + '_' + ObjUserExpenseHeader.ExpenseSheetName;
                    spExpenseSheetName.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spExpenseSheetName.Value = null;
                    spExpenseSheetName.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spExpenseSheetName);

                #endregion

                #region ClientName

                SqlParameter spClientName = new SqlParameter();
                spClientName.ParameterName = "@ClientName";
                if (!string.IsNullOrEmpty(ObjUserExpenseHeader.ClientName))
                {
                    spClientName.Value = ObjUserExpenseHeader.ClientName;
                    spClientName.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spClientName.Value = null;
                    spClientName.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spClientName);

                #endregion

                #region ClientManagerName

                SqlParameter spClientManagerName = new SqlParameter();
                spClientManagerName.ParameterName = "@ClientManagerName";
                if (!string.IsNullOrEmpty(ObjUserExpenseHeader.ClientManagerName))
                {
                    spClientManagerName.Value = ObjUserExpenseHeader.ClientManagerName;
                    spClientManagerName.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spClientManagerName.Value = null;
                    spClientManagerName.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spClientManagerName);

                #endregion

                #region ServiceOrderNo

                SqlParameter spServiceOrderNo = new SqlParameter();
                spServiceOrderNo.ParameterName = "@ServiceOrderNumber";
                if (!string.IsNullOrEmpty(ObjUserExpenseHeader.ServiceOrderNo))
                {
                    spServiceOrderNo.Value = ObjUserExpenseHeader.ServiceOrderNo;
                    spServiceOrderNo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spServiceOrderNo.Value = null;
                    spServiceOrderNo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spServiceOrderNo);

                #endregion

                #region SOExplanation

                SqlParameter spSOExplanation = new SqlParameter();
                spSOExplanation.ParameterName = "@SOExplanation";
                if (!string.IsNullOrEmpty(ObjUserExpenseHeader.ExplanationforSONo))
                {
                    spSOExplanation.Value = ObjUserExpenseHeader.ExplanationforSONo;
                    spSOExplanation.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spSOExplanation.Value = null;
                    spSOExplanation.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spSOExplanation);

                #endregion

                #region SerialNoforInstrument

                SqlParameter spSerialNoforInstrument = new SqlParameter();
                spSerialNoforInstrument.ParameterName = "@SONoforInstrument";
                if (!string.IsNullOrEmpty(ObjUserExpenseHeader.SerialNoforInstrument))
                {
                    spSerialNoforInstrument.Value = ObjUserExpenseHeader.SerialNoforInstrument;
                    spSerialNoforInstrument.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spSerialNoforInstrument.Value = null;
                    spSerialNoforInstrument.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spSerialNoforInstrument);

                #endregion

                #region OrderConfirmationNo

                SqlParameter spOrderConfirmationNo = new SqlParameter();
                spOrderConfirmationNo.ParameterName = "@OrderConfirmationNo";
                if (!string.IsNullOrEmpty(ObjUserExpenseHeader.ServiceOrderConfirmationNo))
                {
                    spOrderConfirmationNo.Value = ObjUserExpenseHeader.ServiceOrderConfirmationNo;
                    spOrderConfirmationNo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spOrderConfirmationNo.Value = null;
                    spOrderConfirmationNo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spOrderConfirmationNo);

                #endregion

                #region OrderConfirmation Date

                SqlParameter spOrderConfirmationDate = new SqlParameter();
                spOrderConfirmationDate.ParameterName = "@OrderConfirmationDate";
                if (ObjUserExpenseHeader.ServiceOrderConfirmationDate != null)
                {
                    spOrderConfirmationDate.Value = ObjUserExpenseHeader.ServiceOrderConfirmationDate;
                    spOrderConfirmationDate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spOrderConfirmationDate.Value = null;
                    spOrderConfirmationDate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spOrderConfirmationDate);

                #endregion

                #region comments
                SqlParameter spComments = new SqlParameter();
                spComments.ParameterName = "@Comments";
                if (ObjeUserExpenseDetails.CommentsforExpenseType != string.Empty)
                {
                    spComments.Value = ObjeUserExpenseDetails.CommentsforExpenseType;
                    spComments.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spComments.Value = null;
                    spComments.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spComments);
                #endregion

                #endregion

                #region details

                #region fromlocation

                SqlParameter spFromLocation = new SqlParameter();
                spFromLocation.ParameterName = "@From";
                if (!string.IsNullOrEmpty(ObjeUserExpenseDetails.FromLocation))
                {
                    spFromLocation.Value = ObjeUserExpenseDetails.FromLocation;
                    spFromLocation.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spFromLocation.Value = null;
                    spFromLocation.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spFromLocation);

                #endregion

                #region tolocation

                SqlParameter spToLocation = new SqlParameter();
                spToLocation.ParameterName = "@To";
                if (!string.IsNullOrEmpty(ObjeUserExpenseDetails.ToLocation))
                {
                    spToLocation.Value = ObjeUserExpenseDetails.ToLocation;
                    spToLocation.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spToLocation.Value = null;
                    spToLocation.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spToLocation);

                #endregion

                #region expensetype

                SqlParameter spFKExpenseType = new SqlParameter();
                spFKExpenseType.ParameterName = "@ExpenditureType";
                if ((ObjeUserExpenseDetails.FK_ExpenseType) != null)
                {
                    spFKExpenseType.Value = ObjeUserExpenseDetails.FK_ExpenseType;
                    spFKExpenseType.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spFKExpenseType.Value = null;
                    spFKExpenseType.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spFKExpenseType);

                #endregion

                #region expensetypedetails

                SqlParameter spExpenditureTypeDetail = new SqlParameter();
                spExpenditureTypeDetail.ParameterName = "@ExpenditureTypeDetail";
                if ((ObjeUserExpenseDetails.FK_ExpenseTypeDetails) != null)
                {
                    spExpenditureTypeDetail.Value = ObjeUserExpenseDetails.FK_ExpenseTypeDetails;
                    spExpenditureTypeDetail.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spExpenditureTypeDetail.Value = null;
                    spExpenditureTypeDetail.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spExpenditureTypeDetail);

                #endregion

                #region travelled kms

                SqlParameter spTravelledKms = new SqlParameter();
                spTravelledKms.ParameterName = "@kms";
                if ((ObjeUserExpenseDetails.TraveledKms) != null)
                {
                    spTravelledKms.Value = ObjeUserExpenseDetails.TraveledKms;
                    spTravelledKms.SqlDbType = SqlDbType.Decimal;
                }
                else
                {
                    spTravelledKms.Value = null;
                    spTravelledKms.SqlDbType = SqlDbType.Decimal;
                }
                paramColl.Add(spTravelledKms);

                #endregion

                #region conveyance

                SqlParameter spConveyance = new SqlParameter();
                spConveyance.ParameterName = "@Conveyance";
                if ((ObjeUserExpenseDetails.Conveyance) != null)
                {
                    spConveyance.Value = ObjeUserExpenseDetails.Conveyance;
                    spConveyance.SqlDbType = SqlDbType.Decimal;
                }
                else
                {
                    spConveyance.Value = null;
                    spConveyance.SqlDbType = SqlDbType.Decimal;
                }
                paramColl.Add(spConveyance);

                #endregion

                #region amount

                SqlParameter spAmount = new SqlParameter();
                spAmount.ParameterName = "@amount";
                if ((ObjeUserExpenseDetails.Amount) != null)
                {
                    spAmount.Value = ObjeUserExpenseDetails.Amount;
                    spAmount.SqlDbType = SqlDbType.Decimal;
                }
                else
                {
                    spAmount.Value = null;
                    spAmount.SqlDbType = SqlDbType.Decimal;
                }
                paramColl.Add(spAmount);

                #endregion

                #region invoiceno

                SqlParameter spInvoiceNo = new SqlParameter();
                spInvoiceNo.ParameterName = "@InvoiceNo";
                if (!string.IsNullOrEmpty(ObjeUserExpenseDetails.InvoiceNo))
                {
                    spInvoiceNo.Value = ObjeUserExpenseDetails.InvoiceNo;
                    spInvoiceNo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spInvoiceNo.Value = null;
                    spInvoiceNo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spInvoiceNo);

                #endregion

                #region ReasonforOther

                SqlParameter spReasonforOther = new SqlParameter();
                spReasonforOther.ParameterName = "@ReasonforOther";
                if (!string.IsNullOrEmpty(ObjeUserExpenseDetails.ReasonForOther))
                {
                    spReasonforOther.Value = ObjeUserExpenseDetails.ReasonForOther;
                    spReasonforOther.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spReasonforOther.Value = null;
                    spReasonforOther.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spReasonforOther);

                #endregion

                #region mobileno

                SqlParameter spMobileNo = new SqlParameter();
                spMobileNo.ParameterName = "@MobileNo";
                if (ObjeUserExpenseDetails.MobileNo != null)
                {
                    spMobileNo.Value = ObjeUserExpenseDetails.MobileNo;
                    spMobileNo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spMobileNo.Value = null;
                    spMobileNo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spMobileNo);

                #endregion

                #endregion

                #endregion

                #region createdby

                SqlParameter spCreatedBy = new SqlParameter();
                spCreatedBy.ParameterName = "@CreatedBy";
                if (emp.FirstName != null)
                {
                    spCreatedBy.Value = emp.FirstName + " " + emp.LastName;
                    spCreatedBy.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spCreatedBy.Value = null;
                    spCreatedBy.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spCreatedBy);

                #endregion

                SqlParameter spExpenseHeaderID = new SqlParameter();
                spExpenseHeaderID.ParameterName = "@ExpenseHeaderID";
                spExpenseHeaderID.SqlDbType = SqlDbType.Int;
                spExpenseHeaderID.Direction = ParameterDirection.Output;
                paramColl.Add(spExpenseHeaderID);

                SqlParameter spDetailId = new SqlParameter();
                spDetailId.ParameterName = "@detailsCount";
                spDetailId.SqlDbType = SqlDbType.Int;
                spDetailId.Direction = ParameterDirection.Output;
                paramColl.Add(spDetailId);

                GDSDBWrapper.ExecuteNonQueryOutParam("SP_CreateExpenditure", paramColl);

                ExpenseHeaderID = int.Parse(spExpenseHeaderID.Value.ToString());

                detailID = int.Parse(spDetailId.Value.ToString());
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            //return detailID;
            return ExpenseHeaderID;
        }

        public DataTable GetTravelList()
        {
            DataTable counttable = new DataTable();
            ArrayList paramRequest = new ArrayList();
            try
            {
                counttable = GDSDBWrapper.ExecuteDataReaderMultiParam("[SP_GetTravelDeskInfo]", paramRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return counttable;


        }

        public DataTable GetExpenditureTypes()
        {
            DataTable dtExpenditureTypes = new DataTable();

            try
            {
                ArrayList ExpenditureType = new ArrayList();

                dtExpenditureTypes = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetExpenditureTypes", ExpenditureType);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return dtExpenditureTypes;
        }

        public DataTable GetExpenditureTypeDetails()
        {
            DataTable dtExpenditureTypeDetails = new DataTable();

            try
            {
                ArrayList ExpenditureTypeDetails = new ArrayList();

                dtExpenditureTypeDetails = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetExpenditureTypeDetails", ExpenditureTypeDetails);

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return dtExpenditureTypeDetails;
        }

        public DataTable GetExpenditureDetailsByID(int? ExpenseHeaderID)
        {
            DataTable dtExpenditureDetailsByID = new DataTable();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter spHeaderID = new SqlParameter();
                spHeaderID.ParameterName = "@ExpenseHeaderID";
                if (ExpenseHeaderID != null)
                {
                    spHeaderID.Value = ExpenseHeaderID;
                    spHeaderID.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spHeaderID.Value = null;
                    spHeaderID.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spHeaderID);

                dtExpenditureDetailsByID = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetUserExpenditureDetailsByID", paramColl);
            }
            catch (Exception Ex)
            {

                throw;
            }

            return dtExpenditureDetailsByID;
        }

        public int? CreateExpenditureDetails(UserExpenses_Header ObjUserExpenseHeader, UserExpense_Details ObjeUserExpenseDetails, Employees emp, out int? detailsID)
        {
            ArrayList paramColl = null;
            int? res = null;

            try
            {
                paramColl = new ArrayList();

                #region header details

                #region
                SqlParameter spHeaderID = new SqlParameter();
                spHeaderID.ParameterName = "@ExpenseHeaderID";
                if ((ObjUserExpenseHeader.Id) != 0)
                {
                    spHeaderID.Value = ObjUserExpenseHeader.Id;
                    spHeaderID.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spHeaderID.Value = null;
                    spHeaderID.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spHeaderID);
                #endregion

                #region employeeID
                SqlParameter spEmployeeId = new SqlParameter();
                spEmployeeId.ParameterName = "@EmployeeID";
                if (ObjUserExpenseHeader.FK_EmployeeId > 0)
                {
                    spEmployeeId.Value = emp.FirstName + ' ' + emp.LastName;
                    spEmployeeId.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spEmployeeId.Value = null;
                    spEmployeeId.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spEmployeeId);
                #endregion

                #region ServiceOrderNo

                SqlParameter spServiceOrderNo = new SqlParameter();
                spServiceOrderNo.ParameterName = "@ServiceOrderNumber";
                if (!string.IsNullOrEmpty(ObjUserExpenseHeader.ServiceOrderNo))
                {
                    spServiceOrderNo.Value = ObjUserExpenseHeader.ServiceOrderNo;
                    spServiceOrderNo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spServiceOrderNo.Value = null;
                    spServiceOrderNo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spServiceOrderNo);

                #endregion

                #region SOExplanation

                SqlParameter spSOExplanation = new SqlParameter();
                spSOExplanation.ParameterName = "@SOExplanation";
                if (!string.IsNullOrEmpty(ObjUserExpenseHeader.ExplanationforSONo))
                {
                    spSOExplanation.Value = ObjUserExpenseHeader.ExplanationforSONo;
                    spSOExplanation.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spSOExplanation.Value = null;
                    spSOExplanation.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spSOExplanation);

                #endregion

                #region OrderConfirmationNo

                SqlParameter spOrderConfirmationNo = new SqlParameter();
                spOrderConfirmationNo.ParameterName = "@OrderConfirmationNo";
                if (!string.IsNullOrEmpty(ObjUserExpenseHeader.ServiceOrderConfirmationNo))
                {
                    spOrderConfirmationNo.Value = ObjUserExpenseHeader.ServiceOrderConfirmationNo;
                    spOrderConfirmationNo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spOrderConfirmationNo.Value = null;
                    spOrderConfirmationNo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spOrderConfirmationNo);

                #endregion

                #region OrderConfirmation Date

                SqlParameter spOrderConfirmationDate = new SqlParameter();
                spOrderConfirmationDate.ParameterName = "@OrderConfirmationDate";
                if (ObjUserExpenseHeader.ServiceOrderConfirmationDate != null)
                {
                    spOrderConfirmationDate.Value = ObjUserExpenseHeader.ServiceOrderConfirmationDate;
                    spOrderConfirmationDate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spOrderConfirmationDate.Value = null;
                    spOrderConfirmationDate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spOrderConfirmationDate);

                #endregion

                #region comments
                SqlParameter spComments = new SqlParameter();
                spComments.ParameterName = "@Comments";
                if (ObjeUserExpenseDetails.CommentsforExpenseType != string.Empty)
                {
                    spComments.Value = ObjeUserExpenseDetails.CommentsforExpenseType;
                    spComments.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spComments.Value = null;
                    spComments.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spComments);
                #endregion

                #endregion

                #region expenditure details

                #region fromlocation

                SqlParameter spFromLocation = new SqlParameter();
                spFromLocation.ParameterName = "@From";
                if (!string.IsNullOrEmpty(ObjeUserExpenseDetails.FromLocation))
                {
                    spFromLocation.Value = ObjeUserExpenseDetails.FromLocation;
                    spFromLocation.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spFromLocation.Value = null;
                    spFromLocation.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spFromLocation);

                #endregion

                #region tolocation

                SqlParameter spToLocation = new SqlParameter();
                spToLocation.ParameterName = "@To";
                if (!string.IsNullOrEmpty(ObjeUserExpenseDetails.ToLocation))
                {
                    spToLocation.Value = ObjeUserExpenseDetails.ToLocation;
                    spToLocation.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spToLocation.Value = null;
                    spToLocation.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spToLocation);

                #endregion

                #region expensetype

                SqlParameter spFKExpenseType = new SqlParameter();
                spFKExpenseType.ParameterName = "@ExpenditureType";
                if ((ObjeUserExpenseDetails.FK_ExpenseType) != null)
                {
                    spFKExpenseType.Value = ObjeUserExpenseDetails.FK_ExpenseType;
                    spFKExpenseType.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spFKExpenseType.Value = null;
                    spFKExpenseType.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spFKExpenseType);

                #endregion

                #region expensetypedetails

                SqlParameter spExpenditureTypeDetail = new SqlParameter();
                spExpenditureTypeDetail.ParameterName = "@ExpenditureTypeDetail";
                if ((ObjeUserExpenseDetails.FK_ExpenseTypeDetails) != null)
                {
                    spExpenditureTypeDetail.Value = ObjeUserExpenseDetails.FK_ExpenseTypeDetails;
                    spExpenditureTypeDetail.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spExpenditureTypeDetail.Value = null;
                    spExpenditureTypeDetail.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spExpenditureTypeDetail);

                #endregion

                #region travelled kms

                SqlParameter spTravelledKms = new SqlParameter();
                spTravelledKms.ParameterName = "@kms";
                if ((ObjeUserExpenseDetails.TraveledKms) != null)
                {
                    spTravelledKms.Value = ObjeUserExpenseDetails.TraveledKms;
                    spTravelledKms.SqlDbType = SqlDbType.Decimal;
                }
                else
                {
                    spTravelledKms.Value = null;
                    spTravelledKms.SqlDbType = SqlDbType.Decimal;
                }
                paramColl.Add(spTravelledKms);

                #endregion

                #region conveyance

                SqlParameter spConveyance = new SqlParameter();
                spConveyance.ParameterName = "@Conveyance";
                if ((ObjeUserExpenseDetails.Conveyance) != null)
                {
                    spConveyance.Value = ObjeUserExpenseDetails.Conveyance;
                    spConveyance.SqlDbType = SqlDbType.Decimal;
                }
                else
                {
                    spConveyance.Value = null;
                    spConveyance.SqlDbType = SqlDbType.Decimal;
                }
                paramColl.Add(spConveyance);

                #endregion

                #region amount

                SqlParameter spAmount = new SqlParameter();
                spAmount.ParameterName = "@amount";
                if ((ObjeUserExpenseDetails.Amount) != null)
                {
                    spAmount.Value = ObjeUserExpenseDetails.Amount;
                    spAmount.SqlDbType = SqlDbType.Decimal;
                }
                else
                {
                    spAmount.Value = null;
                    spAmount.SqlDbType = SqlDbType.Decimal;
                }
                paramColl.Add(spAmount);

                #endregion

                #region invoiceno

                SqlParameter spInvoiceNo = new SqlParameter();
                spInvoiceNo.ParameterName = "@InvoiceNo";
                if (!string.IsNullOrEmpty(ObjeUserExpenseDetails.InvoiceNo))
                {
                    spInvoiceNo.Value = ObjeUserExpenseDetails.InvoiceNo;
                    spInvoiceNo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spInvoiceNo.Value = null;
                    spInvoiceNo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spInvoiceNo);

                #endregion

                #region ReasonforOther

                SqlParameter spReasonforOther = new SqlParameter();
                spReasonforOther.ParameterName = "@ReasonforOther";
                if (!string.IsNullOrEmpty(ObjeUserExpenseDetails.ReasonForOther))
                {
                    spReasonforOther.Value = ObjeUserExpenseDetails.ReasonForOther;
                    spReasonforOther.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spReasonforOther.Value = null;
                    spReasonforOther.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spReasonforOther);

                #endregion

                #region SONoforinstruments

                SqlParameter SONoforinstruments = new SqlParameter();
                SONoforinstruments.ParameterName = "@SONoforinstruments";
                if (!string.IsNullOrEmpty(ObjeUserExpenseDetails.ReasonForOther))
                {
                    SONoforinstruments.Value = ObjeUserExpenseDetails.ReasonForOther;
                    SONoforinstruments.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    SONoforinstruments.Value = null;
                    SONoforinstruments.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(SONoforinstruments);

                #endregion

                #region mobileno

                SqlParameter spMobileNo = new SqlParameter();
                spMobileNo.ParameterName = "@MobileNo";
                if (ObjeUserExpenseDetails.MobileNo != null)
                {
                    spMobileNo.Value = ObjeUserExpenseDetails.MobileNo;
                    spMobileNo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spMobileNo.Value = null;
                    spMobileNo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spMobileNo);

                #endregion

                #endregion

                SqlParameter spDetailId = new SqlParameter();
                spDetailId.ParameterName = "@detailsCount";
                spDetailId.SqlDbType = SqlDbType.Int;
                spDetailId.Direction = ParameterDirection.Output;
                paramColl.Add(spDetailId);

                res = GDSDBWrapper.ExecuteNonQueryOutParam("SP_CreateExpenditureDetail", paramColl);

                detailsID = int.Parse(spDetailId.Value.ToString());

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return res;
        }

        public DataTable GetExpenseType()
        {
            DataTable dtExpenseTypes = new DataTable();

            try
            {
                ArrayList emps = new ArrayList();

                dtExpenseTypes = GDSDBWrapper.ExecuteDataReaderMultiParam("sp_GetExpenseType", emps);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtExpenseTypes;
        }

        public DataSet GetExpenseType(int? id)
        {
            ArrayList paramColl = null;
            DataSet dt = new DataSet();
            try
            {
                paramColl = new ArrayList();

                SqlParameter spgroupId = new SqlParameter();
                spgroupId.ParameterName = "@Id";
                if (id != null)
                {
                    spgroupId.Value = id;
                    spgroupId.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spgroupId.Value = null;
                    spgroupId.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spgroupId);

                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramColl.Add(errorCount);

                dt = GDSDBWrapper.ExecuteCustomDatasetMultiParam("sp_GetExpenseTypeByID", paramColl);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return dt;
        }

        public int? CreateExpenseType(ExpenseType expensetype)
        {
            ArrayList paramColl = null;
            int? result = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter spgroupId = new SqlParameter();
                spgroupId.ParameterName = "@ExpanseName";
                if ((expensetype.ExpenseName) != null)
                {
                    spgroupId.Value = expensetype.ExpenseName;
                    spgroupId.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spgroupId.Value = null;
                    spgroupId.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spgroupId);

                SqlParameter spgroupDesc = new SqlParameter();
                spgroupDesc.ParameterName = "@Description";
                if ((expensetype.Description) != null)
                {
                    spgroupDesc.Value = expensetype.Description;
                    spgroupDesc.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spgroupDesc.Value = null;
                    spgroupDesc.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spgroupDesc);
                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramColl.Add(errorCount);

                if (expensetype.id != null)
                {
                    SqlParameter spzoneid = new SqlParameter();
                    spzoneid.ParameterName = "@Id";
                    spzoneid.Value = expensetype.id;
                    spzoneid.SqlDbType = SqlDbType.Int;
                    paramColl.Add(spzoneid);

                    SqlParameter spUpdateBy = new SqlParameter();
                    spUpdateBy.ParameterName = "@ModifiedBy";
                    spUpdateBy.Value = expensetype.ModifiedBy;
                    spUpdateBy.SqlDbType = SqlDbType.NVarChar;
                    paramColl.Add(spUpdateBy);

                    GDSDBWrapper.ExecuteNonQueryOutParam("sp_UpdateExpanseType", paramColl);
                }
                else
                {

                    SqlParameter spCreatedBy = new SqlParameter();
                    spCreatedBy.ParameterName = "@CreatedBy";
                    spCreatedBy.Value = expensetype.CreatedBy;
                    spCreatedBy.SqlDbType = SqlDbType.NVarChar;
                    paramColl.Add(spCreatedBy);

                    GDSDBWrapper.ExecuteNonQueryOutParam("sp_CreateExpanseType", paramColl);
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;

        }


        public int? ExpenditureSubmit(bool status, int ExpenseHeaderID, string SONo, string SOExplanation, string SerialNoInstrument, string OrderConfirmationNo, string OrderConfirmationDate)
        {
            ArrayList paramColl = null;
            int? res = null;

            try
            {
                paramColl = new ArrayList();

                #region status
                SqlParameter spStatus = new SqlParameter();
                spStatus.ParameterName = "@Status";
                if (status != false)
                {
                    spStatus.Value = true;
                    spStatus.SqlDbType = SqlDbType.Bit;
                }
                else
                {
                    spStatus.Value = false;
                    spStatus.SqlDbType = SqlDbType.Bit;
                }
                paramColl.Add(spStatus);
                #endregion

                #region headerID
                SqlParameter spExpenseHeaderID = new SqlParameter();
                spExpenseHeaderID.ParameterName = "@ExpenseHeaderID";
                if (ExpenseHeaderID > 0)
                {
                    spExpenseHeaderID.Value = ExpenseHeaderID;
                    spExpenseHeaderID.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spExpenseHeaderID.Value = null;
                    spExpenseHeaderID.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spExpenseHeaderID);
                #endregion

                #region SoNo
                SqlParameter spSONo = new SqlParameter();
                spSONo.ParameterName = "@SONo";
                if (SONo != string.Empty)
                {
                    spSONo.Value = SONo;
                    spSONo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spSONo.Value = null;
                    spSONo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spSONo);
                #endregion

                #region SOExplanation
                SqlParameter spSOExplanation = new SqlParameter();
                spSOExplanation.ParameterName = "@SOExplanation";
                if (SOExplanation != string.Empty)
                {
                    spSOExplanation.Value = SOExplanation;
                    spSOExplanation.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spSOExplanation.Value = null;
                    spSOExplanation.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spSOExplanation);
                #endregion

                #region InstrumentNo
                SqlParameter spSerialNoInstrument = new SqlParameter();
                spSerialNoInstrument.ParameterName = "@SerialNoInstrument";
                if (SerialNoInstrument != string.Empty)
                {
                    spSerialNoInstrument.Value = SerialNoInstrument;
                    spSerialNoInstrument.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spSerialNoInstrument.Value = null;
                    spSerialNoInstrument.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spSerialNoInstrument);
                #endregion

                #region OCNo
                SqlParameter spOrderConfirmationNo = new SqlParameter();
                spOrderConfirmationNo.ParameterName = "@OrderConfirmationNo";
                if (OrderConfirmationNo != string.Empty)
                {
                    spOrderConfirmationNo.Value = OrderConfirmationNo;
                    spOrderConfirmationNo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spOrderConfirmationNo.Value = null;
                    spOrderConfirmationNo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spOrderConfirmationNo);
                #endregion

                #region OCNoDate
                SqlParameter spSONoDate = new SqlParameter();
                spSONoDate.ParameterName = "@SONoDate";
                if (OrderConfirmationDate != null)
                {
                    spSONoDate.Value = Convert.ToDateTime(OrderConfirmationDate);
                    spSONoDate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spSONoDate.Value = null;
                    spSONoDate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spSONoDate);
                #endregion

                res = GDSDBWrapper.ExecuteNonQueryOutParam("SP_SubmitExpenditure", paramColl);

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return res;
        }

        public DataTable GetSubmittedExpenditure(int? EmployeeID)
        {
            DataTable dtExpenditureHeader = new DataTable();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();
                SqlParameter spExpenseHeaderID = new SqlParameter();
                spExpenseHeaderID.ParameterName = "@EmployeeID";
                if (EmployeeID != null)
                {
                    spExpenseHeaderID.Value = EmployeeID;
                    spExpenseHeaderID.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spExpenseHeaderID.Value = null;
                    spExpenseHeaderID.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spExpenseHeaderID);

                //ArrayList expenseHeader = new ArrayList();

                dtExpenditureHeader = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetSubmittedExpendituresByEmployee", paramColl);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtExpenditureHeader;
        }

        public DataTable GetDraftedExpenditure(int? EmployeeID)
        {
            DataTable dtExpenditureHeader = new DataTable();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();
                SqlParameter spExpenseHeaderID = new SqlParameter();
                spExpenseHeaderID.ParameterName = "@EmployeeID";
                if (EmployeeID != null)
                {
                    spExpenseHeaderID.Value = EmployeeID;
                    spExpenseHeaderID.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spExpenseHeaderID.Value = null;
                    spExpenseHeaderID.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spExpenseHeaderID);

                //ArrayList expenseHeader = new ArrayList();

                dtExpenditureHeader = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetDraftedExpendituresByEmployee", paramColl);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtExpenditureHeader;
        }

        public DataSet GetTravelDesk(int? id)
        {
            ArrayList paramColl = null;
            DataSet dt = new DataSet();
            try
            {
                paramColl = new ArrayList();

                SqlParameter spgroupId = new SqlParameter();
                spgroupId.ParameterName = "@Id";
                if (id != null)
                {
                    spgroupId.Value = id;
                    spgroupId.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spgroupId.Value = null;
                    spgroupId.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spgroupId);

                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramColl.Add(errorCount);

                dt = GDSDBWrapper.ExecuteCustomDatasetMultiParam("sp_GetTravelDesk", paramColl);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return dt;
        }

        public DataTable GetBlockedDatesById(int? id)
        {
            ArrayList paramColl = null;
            DataTable dt = new DataTable();
            try
            {
                paramColl = new ArrayList();

                SqlParameter spgroupId = new SqlParameter();
                spgroupId.ParameterName = "@Id";
                if (id != null)
                {
                    spgroupId.Value = id;
                    spgroupId.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spgroupId.Value = null;
                    spgroupId.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spgroupId);

                //SqlParameter errorCount = new SqlParameter();
                //errorCount.ParameterName = "@iError";
                //errorCount.SqlDbType = SqlDbType.Int;
                //errorCount.Direction = ParameterDirection.Output;
                //paramColl.Add(errorCount);

                dt = GDSDBWrapper.ExecuteDataReaderMultiParam("sp_GetBlockedDatesById", paramColl);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return dt;
        }



        public DataTable GetExpenditureTypeDetailsByID(int ExpenseTypeHeaderID)
        {
            DataTable dtExpenditureTypeDetailsByID = new DataTable();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();
                SqlParameter spExpenseTypeHeaderID = new SqlParameter();
                spExpenseTypeHeaderID.ParameterName = "@ExpenditureTypeID";
                if (ExpenseTypeHeaderID > 0)
                {
                    spExpenseTypeHeaderID.Value = ExpenseTypeHeaderID;
                    spExpenseTypeHeaderID.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spExpenseTypeHeaderID.Value = null;
                    spExpenseTypeHeaderID.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spExpenseTypeHeaderID);

                dtExpenditureTypeDetailsByID = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetExpenditureTypeDetailsByID", paramColl);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtExpenditureTypeDetailsByID;
        }

        public DataTable ValidateUserId(Employees emp)
        {

            DataTable user = null;
            ArrayList paramRequest = null;
            try
            {
                paramRequest = new ArrayList();
                if (!string.IsNullOrEmpty(emp.Employee_ID))
                {
                    SqlParameter parEmpId = new SqlParameter();
                    parEmpId.ParameterName = "@EmpID";
                    parEmpId.Value = emp.Employee_ID;
                    parEmpId.SqlDbType = SqlDbType.VarChar;
                    paramRequest.Add(parEmpId);
                }




                if (paramRequest.Count > 0)

                    user = GDSDBWrapper.ExecuteDataReaderMultiParam("sp_ValidateUserId", paramRequest);


            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return user;

        }

        public DataTable GetUsersExpendituresforManager(int? EmployeeID, string ExpType)
        {
            DataTable dtExpenditureHeader = new DataTable();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();
                SqlParameter spExpenseHeaderID = new SqlParameter();
                spExpenseHeaderID.ParameterName = "@EmployeeID";
                if (EmployeeID != null)
                {
                    spExpenseHeaderID.Value = EmployeeID;
                    spExpenseHeaderID.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spExpenseHeaderID.Value = null;
                    spExpenseHeaderID.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spExpenseHeaderID);

                SqlParameter spExpType = new SqlParameter();
                spExpType.ParameterName = "@ExpType";
                if (ExpType != null)
                {
                    spExpType.Value = ExpType;
                    spExpType.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spExpType.Value = null;
                    spExpType.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spExpType);

                //ArrayList expenseHeader = new ArrayList();

                dtExpenditureHeader = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetUsersExpendituresforManager", paramColl);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtExpenditureHeader;
        }

        public DataTable GetUsersExpendituresforAccountManager(string ExpType)
        {
            DataTable dtExpenditureHeader = new DataTable();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter spExpType = new SqlParameter();
                spExpType.ParameterName = "@ExpType";
                if (ExpType != null)
                {
                    spExpType.Value = ExpType;
                    spExpType.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spExpType.Value = null;
                    spExpType.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spExpType);

                dtExpenditureHeader = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetUsersExpendituresforAccountManager", paramColl);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtExpenditureHeader;
        }

        public int? UpdateExpenditureStatus(UserExpenses_Header Exp, string userType)
        {
            UserDA userda = new UserDA();

            ArrayList paramColl = null;
            int? result = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@id";
                if ((Exp.Id) != null)
                {
                    paramId.Value = Exp.Id;
                    paramId.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    paramId.Value = null;
                    paramId.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(paramId);

                SqlParameter paramstatus = new SqlParameter();
                paramstatus.ParameterName = "@status";
                if ((Exp.TempStatus) != null)
                {
                    paramstatus.Value = Exp.TempStatus;
                    paramstatus.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    paramstatus.Value = null;
                    paramstatus.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(paramstatus);


                SqlParameter paramcomments = new SqlParameter();
                paramcomments.ParameterName = "@comments";
                if ((Exp.TempComments) != null)
                {
                    paramcomments.Value = Exp.TempComments;
                    paramcomments.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    paramcomments.Value = null;
                    paramcomments.SqlDbType = SqlDbType.NVarChar;
                }

                paramColl.Add(paramcomments);

                SqlParameter paramusertype = new SqlParameter();
                paramusertype.ParameterName = "@usertype";
                if (userType != null)
                {
                    paramusertype.Value = userType;
                    paramusertype.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    paramusertype.Value = null;
                    paramusertype.SqlDbType = SqlDbType.NVarChar;
                }


                paramColl.Add(paramusertype);

                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramColl.Add(errorCount);

                GDSDBWrapper.ExecuteNonQueryOutParam("SP_UpdateExpenditureStatus", paramColl);
                result = int.Parse(errorCount.Value.ToString());
            }
            catch (Exception Ex)
            {
                throw Ex;
            }


            return result;
        }

        public DataTable GetUserExpenses(int? empID, DateTime MaxDate)
        {
            DataTable dtExpenditureHeader = new DataTable();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();
                #region empId
                SqlParameter paramusertype = new SqlParameter();
                paramusertype.ParameterName = "@empID";
                if (empID != null)
                {
                    paramusertype.Value = empID;
                    paramusertype.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    paramusertype.Value = null;
                    paramusertype.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(paramusertype);
                #endregion

                #region expDate
                SqlParameter spMaxDate = new SqlParameter();
                spMaxDate.ParameterName = "@ExpDate";
                if (MaxDate != null)
                {
                    spMaxDate.Value = MaxDate;
                    spMaxDate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spMaxDate.Value = null;
                    spMaxDate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spMaxDate);
                #endregion

                dtExpenditureHeader = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetManagerExpenses", paramColl);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtExpenditureHeader;
        }

        public object CheckExpenditureDateInTraining(DateTime ExpenditureDate, int? EmployeeID)
        {
            object Expendituredate = new object();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter spExpendituredate = new SqlParameter();
                spExpendituredate.ParameterName = "@ExpenditureDate";
                if (ExpenditureDate != null)
                {
                    spExpendituredate.Value = ExpenditureDate;
                    spExpendituredate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spExpendituredate = null;
                    spExpendituredate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spExpendituredate);


                SqlParameter spEmployeeID = new SqlParameter();
                spEmployeeID.ParameterName = "@EmployeeID";
                if (EmployeeID != null)
                {
                    spEmployeeID.Value = EmployeeID;
                    spEmployeeID.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spEmployeeID.Value = null;
                    spEmployeeID.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spEmployeeID);

                Expendituredate = GDSDBWrapper.ExecuteScalar("SP_ValidateExpenditureDateInTraining", paramColl);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Expendituredate;
        }

        public DataTable LeaveDetailsById(int id)
        {

            DataTable LeaveDetailsById = new DataTable();
            ArrayList paramlist = null;
            try
            {
                paramlist = new ArrayList();

                SqlParameter paramid = new SqlParameter();
                paramid.ParameterName = "@id";
                if (id != null)
                {
                    paramid.Value = id;
                    paramid.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    paramid.Value = null;
                    paramid.SqlDbType = SqlDbType.Int;
                }


                paramlist.Add(paramid);


                LeaveDetailsById = GDSDBWrapper.ExecuteDataReaderMultiParam("LeaveDetailsById", paramlist);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return LeaveDetailsById;

        }

        public int? UpdateLeaveStatus(Leave LeaveDetails)
        {
            ArrayList paramlv = null;
            int? UpdatedResult = null;

            try
            {
                paramlv = new ArrayList();
                SqlParameter leaveId = new SqlParameter();
                leaveId.ParameterName = "@id";
                if (LeaveDetails.Id != null)
                {
                    leaveId.Value = LeaveDetails.Id;
                    leaveId.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    leaveId.Value = null;
                    leaveId.SqlDbType = SqlDbType.Int;
                }


                paramlv.Add(leaveId);

                SqlParameter leaveStatus = new SqlParameter();
                leaveStatus.ParameterName = "@Status";
                if (LeaveDetails.LeaveStatus != null)
                {
                    leaveStatus.Value = LeaveDetails.LeaveStatus;
                    leaveStatus.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    leaveStatus.Value = null;
                    leaveStatus.SqlDbType = SqlDbType.Int;
                }


                paramlv.Add(leaveStatus);


                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramlv.Add(errorCount);

                GDSDBWrapper.ExecuteNonQueryOutParam("UpdateLeaveStatus", paramlv);

                UpdatedResult = int.Parse(errorCount.Value.ToString());

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return UpdatedResult;


        }


        public DataTable ValidateLeaveDates(DateTime? sdate, DateTime? edate, int? empid)
        {
            DataTable counttable = new DataTable();
            ArrayList paramRequest = new ArrayList();
            try
            {
                SqlParameter paramsdate = new SqlParameter();
                paramsdate.ParameterName = "@startdate";
                if (sdate != null)
                {


                    paramsdate.Value = sdate;
                    paramsdate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    paramsdate.Value = sdate;
                    paramsdate.SqlDbType = SqlDbType.DateTime;
                }

                paramRequest.Add(paramsdate);

                SqlParameter paramedate = new SqlParameter();
                paramedate.ParameterName = "@enddate";

                if (edate != null)
                {
                    paramedate.Value = edate;
                    paramedate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    paramedate.Value = edate;
                    paramedate.SqlDbType = SqlDbType.DateTime;
                }


                paramRequest.Add(paramedate);


                SqlParameter paramempid = new SqlParameter();
                paramempid.ParameterName = "@empid";
                if (sdate != null)
                {
                    paramempid.Value = empid;
                    paramempid.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    paramempid.Value = null;
                    paramempid.SqlDbType = SqlDbType.Int;
                }


                paramRequest.Add(paramempid);


                counttable = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_ValidateLeaveDates", paramRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return counttable;


        }


        public DataTable ValidateTravelDeskDates(DateTime sdate, DateTime edate, int? empid)
        {
            DataTable counttable = new DataTable();
            ArrayList paramRequest = new ArrayList();
            try
            {
                SqlParameter paramsdate = new SqlParameter();
                paramsdate.ParameterName = "@fromdate";
                if (sdate != null)
                {
                    paramsdate.Value = sdate;
                    paramsdate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    paramsdate.Value = DateTime.Now;
                    paramsdate.SqlDbType = SqlDbType.DateTime;
                }

                paramRequest.Add(paramsdate);

                SqlParameter paramedate = new SqlParameter();
                paramedate.ParameterName = "@todate";
                if (sdate != null)
                {
                    paramedate.Value = edate;
                    paramedate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    paramedate.Value = DateTime.Now;
                    paramedate.SqlDbType = SqlDbType.DateTime;
                }


                paramRequest.Add(paramedate);


                SqlParameter paramempid = new SqlParameter();
                paramempid.ParameterName = "@empid";
                if (sdate != null)
                {
                    paramempid.Value = empid;
                    paramempid.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    paramempid.Value = null;
                    paramempid.SqlDbType = SqlDbType.Int;
                }


                paramRequest.Add(paramempid);


                counttable = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_ValidateTravelDeskDates", paramRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return counttable;


        }



        public DataTable GetManagerActionExpenses(int? EmpID, DateTime MaxDate)
        {
            DataTable dtExpenditureHeader = new DataTable();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();
                #region empId
                SqlParameter paramusertype = new SqlParameter();
                paramusertype.ParameterName = "@empID";
                if (EmpID != null)
                {
                    paramusertype.Value = EmpID;
                    paramusertype.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    paramusertype.Value = null;
                    paramusertype.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(paramusertype);
                #endregion

                #region expDate
                SqlParameter spMaxDate = new SqlParameter();
                spMaxDate.ParameterName = "@ExpDate";
                if (MaxDate != null)
                {
                    spMaxDate.Value = MaxDate;
                    spMaxDate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spMaxDate.Value = null;
                    spMaxDate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spMaxDate);
                #endregion

                dtExpenditureHeader = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetAccountManagerExpenses", paramColl);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtExpenditureHeader;
        }

        public DataTable ExpenseBlockedDatesList()
        {
            DataTable datatable = new DataTable();

            try
            {
                ArrayList emps = new ArrayList();

                datatable = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_ExpenseBlockedDatesList", emps);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return datatable;
        }

        public int? UpdatePaidAmount(string HeaderID, string PaidAmount)
        {
            ArrayList paramlv = null;
            int? UpdatedResult = null;

            try
            {
                paramlv = new ArrayList();
                SqlParameter leaveId = new SqlParameter();
                leaveId.ParameterName = "@id";
                if (HeaderID != null)
                {
                    leaveId.Value = Convert.ToInt32(HeaderID);
                    leaveId.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    leaveId.Value = null;
                    leaveId.SqlDbType = SqlDbType.Int;
                }
                paramlv.Add(leaveId);

                SqlParameter leaveStatus = new SqlParameter();
                leaveStatus.ParameterName = "@PaidAmount";
                if (PaidAmount != null)
                {
                    leaveStatus.Value = Convert.ToSingle(PaidAmount);
                    leaveStatus.SqlDbType = SqlDbType.Float;
                }
                else
                {
                    leaveStatus.Value = null;
                    leaveStatus.SqlDbType = SqlDbType.Float;
                }
                paramlv.Add(leaveStatus);

                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramlv.Add(errorCount);

                GDSDBWrapper.ExecuteNonQueryOutParam("SP_UpdateExpenditurePaidAmount", paramlv);

                UpdatedResult = int.Parse(errorCount.Value.ToString());

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return UpdatedResult;
        }

        public DataTable GetAllEmployeesSubmittedExpenditureRecords()
        {
            DataTable dtAllExpenses = new DataTable();

            try
            {
                ArrayList expenses = new ArrayList();

                dtAllExpenses = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetAllEmployeesSubmittedRecords", expenses);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtAllExpenses;
        }

        public object CheckExpenditureDateInBlockedList(DateTime ExpenditureDate, int? EmployeeID)
        {
            object Expendituredate = new object();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter spExpendituredate = new SqlParameter();
                spExpendituredate.ParameterName = "@ExpenditureDate";
                if (ExpenditureDate != null)
                {
                    spExpendituredate.Value = ExpenditureDate;
                    spExpendituredate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spExpendituredate = null;
                    spExpendituredate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spExpendituredate);


                SqlParameter spEmployeeID = new SqlParameter();
                spEmployeeID.ParameterName = "@EmployeeID";
                if (EmployeeID != null)
                {
                    spEmployeeID.Value = EmployeeID;
                    spEmployeeID.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spEmployeeID.Value = null;
                    spEmployeeID.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spEmployeeID);

                Expendituredate = GDSDBWrapper.ExecuteScalar("SP_ValidateExpenditureInBlockedDates", paramColl);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Expendituredate;
        }

        public DataTable GetClientManagerNames()
        {
            DataTable dtClientManagerName = new DataTable();

            try
            {
                ArrayList ExpenditureTypeDetails = new ArrayList();

                dtClientManagerName = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetClientManagers", ExpenditureTypeDetails);

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return dtClientManagerName;
        }

        public object CheckMobileExpenditure(DateTime ExpenditureDate, int? EmployeeID)
        {
            object Expendituredate = new object();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter spExpendituredate = new SqlParameter();
                spExpendituredate.ParameterName = "@ExpenditureDate";
                if (ExpenditureDate != null)
                {
                    spExpendituredate.Value = ExpenditureDate;
                    spExpendituredate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spExpendituredate = null;
                    spExpendituredate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spExpendituredate);


                SqlParameter spEmployeeID = new SqlParameter();
                spEmployeeID.ParameterName = "@EmployeeID";
                if (EmployeeID != null)
                {
                    spEmployeeID.Value = EmployeeID;
                    spEmployeeID.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spEmployeeID.Value = null;
                    spEmployeeID.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spEmployeeID);

                Expendituredate = GDSDBWrapper.ExecuteScalar("SP_ValidateMobileExpenseSubmit", paramColl);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Expendituredate;
        }

        public object CheckInternetExpenditure(DateTime ExpenditureDate, int? EmployeeID)
        {
            object Expendituredate = new object();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter spExpendituredate = new SqlParameter();
                spExpendituredate.ParameterName = "@ExpenditureDate";
                if (ExpenditureDate != null)
                {
                    spExpendituredate.Value = ExpenditureDate;
                    spExpendituredate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spExpendituredate = null;
                    spExpendituredate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spExpendituredate);


                SqlParameter spEmployeeID = new SqlParameter();
                spEmployeeID.ParameterName = "@EmployeeID";
                if (EmployeeID != null)
                {
                    spEmployeeID.Value = EmployeeID;
                    spEmployeeID.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spEmployeeID.Value = null;
                    spEmployeeID.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spEmployeeID);

                Expendituredate = GDSDBWrapper.ExecuteScalar("SP_ValidateInternetExpenseSubmit", paramColl);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Expendituredate;
        }

        public DataTable GetInstrumentNoExpenditures(int? EmployeeID)
        {
            DataTable dtAllExpenses = new DataTable();

            try
            {
                ArrayList expenses = new ArrayList();

                SqlParameter spEmployeeID = new SqlParameter();
                spEmployeeID.ParameterName = "@EmployeeID";
                if (EmployeeID != null)
                {
                    spEmployeeID.Value = EmployeeID;
                    spEmployeeID.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spEmployeeID.Value = null;
                    spEmployeeID.SqlDbType = SqlDbType.Int;
                }
                expenses.Add(spEmployeeID);

                dtAllExpenses = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetSOandOCNos", expenses);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtAllExpenses;
        }

        public int? UpdateOrderConfirmationNo(UserExpenses_Header objUserExpenseHeader)
        {
            ArrayList paramlv = null;
            int? UpdatedResult = null;

            try
            {
                paramlv = new ArrayList();
                SqlParameter ExpenseHeaderID = new SqlParameter();
                ExpenseHeaderID.ParameterName = "@id";
                if (objUserExpenseHeader.Id > 0)
                {
                    ExpenseHeaderID.Value = objUserExpenseHeader.Id;
                    ExpenseHeaderID.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    ExpenseHeaderID.Value = null;
                    ExpenseHeaderID.SqlDbType = SqlDbType.Int;
                }
                paramlv.Add(ExpenseHeaderID);

                SqlParameter spSONo = new SqlParameter();
                spSONo.ParameterName = "@SONo";
                if (objUserExpenseHeader.ServiceOrderNo != null)
                {
                    spSONo.Value = objUserExpenseHeader.ServiceOrderNo;
                    spSONo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spSONo.Value = null;
                    spSONo.SqlDbType = SqlDbType.VarChar;
                }
                paramlv.Add(spSONo);

                SqlParameter spOCNO = new SqlParameter();
                spOCNO.ParameterName = "@OCNO";
                if (objUserExpenseHeader.ServiceOrderConfirmationNo != null)
                {
                    spOCNO.Value = objUserExpenseHeader.ServiceOrderConfirmationNo;
                    spOCNO.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spOCNO.Value = null;
                    spOCNO.SqlDbType = SqlDbType.VarChar;
                }
                paramlv.Add(spOCNO);

                SqlParameter spODate = new SqlParameter();
                spODate.ParameterName = "@OCDate";
                if (objUserExpenseHeader.ServiceOrderConfirmationDate != null)
                {
                    spODate.Value = objUserExpenseHeader.ServiceOrderConfirmationDate;
                    spODate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spODate.Value = null;
                    spODate.SqlDbType = SqlDbType.DateTime;
                }
                paramlv.Add(spODate);

                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramlv.Add(errorCount);

                GDSDBWrapper.ExecuteNonQueryOutParam("SP_UpdateExpenditureSOCNo", paramlv);

                UpdatedResult = int.Parse(errorCount.Value.ToString());

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return UpdatedResult;
        }

        public DataTable GetPaidExpenditures()
        {
            DataTable dtData = new DataTable();

            ArrayList paramlv = new ArrayList();

            try
            {
                dtData = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetPaidExpenditures", paramlv);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return dtData;
        }

        public object VerifyOldPassword(string password, int? id)
        {
            object oldpassword = new object();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();
                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@ID";
                if (id != null)
                {
                    paramId.Value = id;
                    paramId.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    paramId.Value = null;
                    paramId.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(paramId);


                SqlParameter spverifingpassword = new SqlParameter();
                spverifingpassword.ParameterName = "@Password";
                if (password != null)
                {
                    spverifingpassword.Value = password;
                    spverifingpassword.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spverifingpassword = null;
                    spverifingpassword.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spverifingpassword);

                oldpassword = GDSDBWrapper.ExecuteScalar("SP_VerifyOldPassword", paramColl);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return oldpassword;
        }



        public object updatepassword(string password, int? id)
        {
            object newpassword = new object();
            ArrayList paramColl = null;
            //int? id = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@ID";
                if (id != null)
                {
                    paramId.Value = id;
                    paramId.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    paramId.Value = null;
                    paramId.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(paramId);

                SqlParameter spnewpassword = new SqlParameter();
                spnewpassword.ParameterName = "@Password";
                if (password != null)
                {
                    spnewpassword.Value = password;
                    spnewpassword.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spnewpassword = null;
                    spnewpassword.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spnewpassword);

                newpassword = GDSDBWrapper.ExecuteScalar("updatepassword", paramColl);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return newpassword;
        }



        public int? UpdateProfileImage(Employees employee)
        {

            ArrayList param = null;
            int? UpdatedResult = null;
            try
            {
                param = new ArrayList();
                SqlParameter parampath = new SqlParameter();
                parampath.ParameterName = "@filename";
                if (employee.Temimagepath != null)
                {
                    parampath.Value = employee.Temimagepath;
                    parampath.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    parampath.Value = null;
                    parampath.SqlDbType = SqlDbType.NVarChar;
                }
                param.Add(parampath);

                SqlParameter paramempid = new SqlParameter();
                paramempid.ParameterName = "@empid";
                if (employee.Employee_ID != null)
                {
                    paramempid.Value = employee.Employee_ID;
                    paramempid.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    paramempid.Value = null;
                    paramempid.SqlDbType = SqlDbType.NVarChar;
                }
                param.Add(paramempid);


                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                param.Add(errorCount);

                GDSDBWrapper.ExecuteNonQueryOutParam("sp_UpdateProfileImage", param);

                UpdatedResult = int.Parse(errorCount.Value.ToString());
            }
            catch
            {


            }


            return UpdatedResult;
        }


        public int InsertMultipleServiceOrderNos(int? ExpenseHeaderID, ServiceOrderNos objServiceOrderNosList, Employees emp)
        {
            int resMulti = 0;
            ArrayList paramColl = null;

            try
            {


                //for (int i = 0; i < objServiceOrderNosList.Count; i++)
                //{
                paramColl = new ArrayList();

                SqlParameter spExpenseHeaderID = new SqlParameter();
                spExpenseHeaderID.ParameterName = "@ExpenseHeaderID";

                SqlParameter spCreatedBy = new SqlParameter();
                spCreatedBy.ParameterName = "@CreatedBy";

                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";

                #region ExpenseHeaderId

                if (ExpenseHeaderID != null)
                {
                    spExpenseHeaderID.Value = ExpenseHeaderID;
                    spExpenseHeaderID.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spExpenseHeaderID.Value = null;
                    spExpenseHeaderID.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spExpenseHeaderID);
                #endregion

                #region CreatedBy

                if (emp.FirstName != null)
                {
                    spCreatedBy.Value = emp.FirstName + " " + emp.LastName;
                    spCreatedBy.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spCreatedBy.Value = null;
                    spCreatedBy.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spCreatedBy);
                #endregion

                #region errorCount

                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramColl.Add(errorCount);
                #endregion

                #region ServiceOrderNumber
                SqlParameter spServiceOrderNo = new SqlParameter();
                spServiceOrderNo.ParameterName = "@ServiceOrderNo";
                if (objServiceOrderNosList.ServiceOrderNo != null && objServiceOrderNosList.ServiceOrderNo.Length > 0)
                {
                    spServiceOrderNo.Value = objServiceOrderNosList.ServiceOrderNo;
                    spServiceOrderNo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spServiceOrderNo.Value = null;
                    spServiceOrderNo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spServiceOrderNo);
                #endregion

                #region ServiceOrderConfirmationNo
                SqlParameter spSOConfirmationNo = new SqlParameter();
                spSOConfirmationNo.ParameterName = "@SOCNo";
                if (objServiceOrderNosList.ServiceOrderConfirmationNo != null && objServiceOrderNosList.ServiceOrderConfirmationNo.Length > 0)
                {
                    spSOConfirmationNo.Value = objServiceOrderNosList.ServiceOrderConfirmationNo;
                    spSOConfirmationNo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spSOConfirmationNo.Value = null;
                    spSOConfirmationNo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spSOConfirmationNo);
                #endregion

                #region ServiceOrderConfirmationDate
                SqlParameter spSOConfirmationDate = new SqlParameter();
                spSOConfirmationDate.ParameterName = "@SOCDate";
                if (objServiceOrderNosList.ServiceOrderConfirmationDate != null)
                {
                    spSOConfirmationDate.Value = objServiceOrderNosList.ServiceOrderConfirmationDate;
                    spSOConfirmationDate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spSOConfirmationDate.Value = null;
                    spSOConfirmationDate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spSOConfirmationDate);
                #endregion

                #region ExplanationForSONo
                SqlParameter spExplanationForSONo = new SqlParameter();
                spExplanationForSONo.ParameterName = "@ExplanationForSONo";
                if (objServiceOrderNosList.ExplanationforSONo != null && objServiceOrderNosList.ExplanationforSONo.Length > 0)
                {
                    spExplanationForSONo.Value = objServiceOrderNosList.ExplanationforSONo;
                    spExplanationForSONo.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spExplanationForSONo.Value = null;
                    spExplanationForSONo.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spExplanationForSONo);
                #endregion

                #region InstrumentNumber
                SqlParameter spInstrumentNumber = new SqlParameter();
                spInstrumentNumber.ParameterName = "@InstrumentNumber";
                if (objServiceOrderNosList.SerialNoforInstrument != null && objServiceOrderNosList.SerialNoforInstrument.Length > 0)
                {
                    spInstrumentNumber.Value = objServiceOrderNosList.SerialNoforInstrument;
                    spInstrumentNumber.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spInstrumentNumber.Value = null;
                    spInstrumentNumber.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spInstrumentNumber);
                #endregion

                resMulti = GDSDBWrapper.ExecuteNonQueryOutParam("sp_CreateMultipleSONos", paramColl);
                //}
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return resMulti;
        }

        public DataTable GetMultipleSONOsbyExpenseHeaderID(int? ExpenseHeaderID)
        {
            DataTable dtData = new DataTable();
            ArrayList paramColl;

            try
            {
                paramColl = new ArrayList();
                SqlParameter spExpenseHeaderID = new SqlParameter();
                spExpenseHeaderID.ParameterName = "@ExpenseHeaderID";
                if (ExpenseHeaderID != null)
                {
                    spExpenseHeaderID.Value = ExpenseHeaderID;
                    spExpenseHeaderID.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spExpenseHeaderID.Value = null;
                    spExpenseHeaderID.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spExpenseHeaderID);

                dtData = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetMultipleSONos", paramColl);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtData;
        }

        public DataTable GetEmployeesByDOB(int date, int month)
        {
            DataTable dtData = new DataTable();
            ArrayList paramColl;

            try
            {
                paramColl = new ArrayList();
                SqlParameter spDate = new SqlParameter();
                spDate.ParameterName = "@Date";
                if (date != null)
                {
                    spDate.Value = date;
                    spDate.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spDate.Value = null;
                    spDate.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spDate);


                SqlParameter spMonth = new SqlParameter();
                spMonth.ParameterName = "@Month";
                if (month != null)
                {
                    spMonth.Value = month;
                    spMonth.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spMonth.Value = null;
                    spMonth.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spMonth);
                dtData = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetEmployeesByDOB", paramColl);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtData;
        }


        public int? DeleteMultipleSONos(string SONOID)
        {
            int resMulti = 0;
            ArrayList paramColl = null;

            try
            {
                paramColl = new ArrayList();
                SqlParameter spExpenseHeaderID = new SqlParameter();
                spExpenseHeaderID.ParameterName = "@MultipltSONOID";
                if (SONOID != null && SONOID.Length > 0)
                {
                    spExpenseHeaderID.Value = Convert.ToInt32(SONOID);
                    spExpenseHeaderID.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spExpenseHeaderID.Value = null;
                    spExpenseHeaderID.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spExpenseHeaderID);

                resMulti = GDSDBWrapper.ExecuteNonQueryOutParam("sp_DeleteMultipleSONOs", paramColl);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return resMulti;
        }


        public int? DeleteEventImages(EventImages eventImages)
        {
            int resMulti = 0;
            ArrayList paramColl = null;

            try
            {
                paramColl = new ArrayList();
                SqlParameter speventId = new SqlParameter();
                speventId.ParameterName = "@Fk_EventId";
                if (eventImages.Fk_EventId != null)
                {
                    speventId.Value = eventImages.Fk_EventId;
                    speventId.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    speventId.Value = null;
                    speventId.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(speventId);

                SqlParameter speventImage = new SqlParameter();
                speventImage.ParameterName = "@ImageName";
                if (eventImages.ImageName != null)
                {
                    speventImage.Value = eventImages.ImageName;
                    speventImage.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    speventImage.Value = null;
                    speventImage.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(speventImage);

                resMulti = GDSDBWrapper.ExecuteNonQueryOutParam("sp_DeleteEventImages", paramColl);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return resMulti;
        }


        public int UpdateMultipleSONOs(ServiceOrderNos objserviceOrderNos)
        {
            ArrayList paramColl = null;
            int result;

            try
            {
                paramColl = new ArrayList();

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@id";
                if (objserviceOrderNos.Id > 0)
                {
                    paramId.Value = objserviceOrderNos.Id;
                    paramId.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    paramId.Value = null;
                    paramId.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(paramId);

                SqlParameter paramstatus = new SqlParameter();
                paramstatus.ParameterName = "@ServiceOrderNo";
                if (!(string.IsNullOrWhiteSpace(objserviceOrderNos.ServiceOrderNo)))
                {
                    paramstatus.Value = objserviceOrderNos.ServiceOrderNo;
                    paramstatus.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    paramstatus.Value = null;
                    paramstatus.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(paramstatus);


                SqlParameter paramcomments = new SqlParameter();
                paramcomments.ParameterName = "@ServiceConfirmationNo";
                if (!(string.IsNullOrWhiteSpace(objserviceOrderNos.ServiceOrderConfirmationNo)))
                {
                    paramcomments.Value = objserviceOrderNos.ServiceOrderConfirmationNo;
                    paramcomments.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    paramcomments.Value = null;
                    paramcomments.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(paramcomments);


                SqlParameter paramusertype = new SqlParameter();
                paramusertype.ParameterName = "@ServiceConfirmationDate";
                if (objserviceOrderNos.ServiceOrderConfirmationDate != null)
                {
                    paramusertype.Value = objserviceOrderNos.ServiceOrderConfirmationDate;
                    paramusertype.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    paramusertype.Value = null;
                    paramusertype.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(paramusertype);


                SqlParameter spexplanation = new SqlParameter();
                spexplanation.ParameterName = "@ExplanationForSO";
                if (!(string.IsNullOrWhiteSpace(objserviceOrderNos.ExplanationforSONo)))
                {
                    spexplanation.Value = objserviceOrderNos.ExplanationforSONo;
                    spexplanation.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spexplanation.Value = null;
                    spexplanation.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spexplanation);


                SqlParameter spInstrument = new SqlParameter();
                spInstrument.ParameterName = "@InstrumentNo";
                if (!(string.IsNullOrWhiteSpace(objserviceOrderNos.SerialNoforInstrument)))
                {
                    spInstrument.Value = objserviceOrderNos.SerialNoforInstrument;
                    spInstrument.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spInstrument.Value = null;
                    spInstrument.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spInstrument);




                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramColl.Add(errorCount);

                GDSDBWrapper.ExecuteNonQueryOutParam("SP_UpdateMultipleSONOs", paramColl);
                result = int.Parse(errorCount.Value.ToString());
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }

        public DataTable GetExpenditureReport(DateTime ExpenditureSDate, DateTime ExpenditureEDate, int? EmpID)
        {
            DataTable dtExpenditureHeader = new DataTable();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();

                #region empId
                SqlParameter paramusertype = new SqlParameter();
                paramusertype.ParameterName = "@empID";
                if (EmpID != null)
                {
                    paramusertype.Value = EmpID;
                    paramusertype.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    paramusertype.Value = 0;
                    paramusertype.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(paramusertype);
                #endregion

                #region expSDate
                SqlParameter spMaxDate = new SqlParameter();
                spMaxDate.ParameterName = "@ExpSDate";
                if (ExpenditureSDate != null)
                {
                    spMaxDate.Value = ExpenditureSDate;
                    spMaxDate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spMaxDate.Value = null;
                    spMaxDate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spMaxDate);
                #endregion

                #region expEDate
                SqlParameter spEDate = new SqlParameter();
                spEDate.ParameterName = "@ExpEDate";
                if (ExpenditureEDate != null)
                {
                    spEDate.Value = ExpenditureEDate;
                    spEDate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spEDate.Value = null;
                    spEDate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spEDate);
                #endregion

                dtExpenditureHeader = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetEmployeeExpenseReport", paramColl);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtExpenditureHeader;
        }

        public DataTable GetExpenditureReportByServiceOrderNo(int value)
        {
            DataTable dtExpenditureHeader = new DataTable();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();
                if (value == 1)
                    dtExpenditureHeader = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_ExpenditureReportByServiceOrderNo", paramColl);
                else
                    dtExpenditureHeader = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_AllExpenseswithSONumbers", paramColl);

            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtExpenditureHeader;
        }


        public DataTable GetExpenditureNonMatchingSONumbers(string Soder, string Soc)
        {
            DataTable dtExpenditureHeader = new DataTable();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();


                SqlParameter paramSoder = new SqlParameter();
                paramSoder.ParameterName = "@Soder";
                paramSoder.Value = Soder;
                paramSoder.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(paramSoder);

                SqlParameter paramSoc = new SqlParameter();
                paramSoc.ParameterName = "@Soc";
                paramSoc.Value = Soc;
                paramSoc.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(paramSoc);

                dtExpenditureHeader = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetExpenditureNonMatchingSONumbers", paramColl);

            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtExpenditureHeader;
        }

        public DataSet GetJobsList()
        {
            DataSet ds = new DataSet();
            try
            {
                ArrayList emp = new ArrayList();
                ds = GDSDBWrapper.ExecuteCustomDatasetMultiParam("SP_GetJobsList", emp);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return ds;
        }

      
        public DataTable EventImagesListById(int? id)
        {
            DataTable dt = new DataTable();
            ArrayList paramlist = null;
            try
            {
                paramlist = new ArrayList();
                SqlParameter evId = new SqlParameter();
                evId.ParameterName = "@EventId";
                if (id != null)
                {
                    evId.Value = id;
                    evId.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    evId.Value = null;
                    evId.SqlDbType = SqlDbType.Int;
                }
                paramlist.Add(evId);


                dt = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetEventImagesListById", paramlist);
            }
            catch (Exception EX)
            {
                throw EX;
            }

            return dt;
        }
        public int? PostNewJob(Jobs jobs, int flag)
        {
            ArrayList paramColl = null;
            int? result = null;
            try
            {
                paramColl = new ArrayList();

                #region parameters


                SqlParameter spJobTitle = new SqlParameter();
                spJobTitle.ParameterName = "@JobTitle";
                if (!string.IsNullOrEmpty(jobs.JobTitle))
                {
                    spJobTitle.Value = jobs.JobTitle.Trim();
                    spJobTitle.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spJobTitle.Value = null;
                    spJobTitle.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spJobTitle);


                SqlParameter spJobDescription = new SqlParameter();
                spJobDescription.ParameterName = "@JobDescription";
                if (!string.IsNullOrEmpty(jobs.JobDescription))
                {
                    spJobDescription.Value = jobs.JobDescription.Trim();
                    spJobTitle.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spJobDescription.Value = null;
                    spJobDescription.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spJobDescription);

                SqlParameter spJobDetails = new SqlParameter();
                spJobDetails.ParameterName = "@JobDetails";
                if (!string.IsNullOrEmpty(jobs.JobDetails))
                {
                    spJobDetails.Value = jobs.JobDetails.Trim();
                    spJobDetails.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spJobDetails.Value = null;
                    spJobDetails.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spJobDetails);


                SqlParameter spMinExperience = new SqlParameter();
                spMinExperience.ParameterName = "@MinExperience";
                if ((jobs.MinExperience) != null)
                {
                    spMinExperience.Value = jobs.MinExperience;
                    spMinExperience.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spMinExperience.Value = null;
                    spMinExperience.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spMinExperience);

                SqlParameter spMaxExperience = new SqlParameter();
                spMaxExperience.ParameterName = "@MaxExperience";
                if ((jobs.MaxExperience) != null)
                {
                    spMaxExperience.Value = jobs.MaxExperience;
                    spMaxExperience.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spMaxExperience.Value = null;
                    spMaxExperience.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spMaxExperience);


                SqlParameter spJobLocation = new SqlParameter();
                spJobLocation.ParameterName = "@JobLocation";
                if (!string.IsNullOrEmpty(jobs.JobLocation))
                {
                    spJobLocation.Value = jobs.JobLocation.Trim();
                    spJobLocation.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spJobLocation.Value = null;
                    spJobLocation.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spJobLocation);


                SqlParameter spIndustry = new SqlParameter();
                spIndustry.ParameterName = "@Industry";
                if (!string.IsNullOrEmpty(jobs.Industry))
                {
                    spIndustry.Value = jobs.Industry.Trim();
                    spIndustry.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spIndustry.Value = null;
                    spIndustry.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spIndustry);

                SqlParameter spRole = new SqlParameter();
                spRole.ParameterName = "@Role";
                if (!string.IsNullOrEmpty(jobs.Role))
                {
                    spRole.Value = jobs.Role.Trim();
                    spRole.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spRole.Value = null;
                    spRole.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spRole);

                SqlParameter spEducation = new SqlParameter();
                spEducation.ParameterName = "@Education";
                if (!string.IsNullOrEmpty(jobs.Education))
                {
                    spEducation.Value = jobs.Education.Trim();
                    spEducation.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spEducation.Value = null;
                    spEducation.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spEducation);


                SqlParameter spSpecialization = new SqlParameter();
                spSpecialization.ParameterName = "@Specialization";
                if (!string.IsNullOrEmpty(jobs.Specialization))
                {
                    spSpecialization.Value = jobs.Specialization.Trim();
                    spSpecialization.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spSpecialization.Value = null;
                    spSpecialization.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spSpecialization);

                SqlParameter spContactName = new SqlParameter();
                spContactName.ParameterName = "@ContactName";
                if (!string.IsNullOrEmpty(jobs.ContactName))
                {
                    spContactName.Value = jobs.ContactName.Trim();
                    spContactName.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spContactName.Value = null;
                    spContactName.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spContactName);

                SqlParameter spContactEmail = new SqlParameter();
                spContactEmail.ParameterName = "@ContactEmail";
                if (!string.IsNullOrEmpty(jobs.ContactEmail))
                {
                    spContactEmail.Value = jobs.ContactEmail.Trim();
                    spContactEmail.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spContactEmail.Value = null;
                    spContactEmail.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spContactEmail);

                SqlParameter spContactPhoneNumber = new SqlParameter();
                spContactPhoneNumber.ParameterName = "@ContactPhoneNumber";
                if (!string.IsNullOrEmpty(jobs.ContactPhoneNumber))
                {
                    spContactPhoneNumber.Value = jobs.ContactPhoneNumber.Trim();
                    spContactPhoneNumber.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spContactPhoneNumber.Value = null;
                    spContactPhoneNumber.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spContactPhoneNumber);


                SqlParameter spIsActive = new SqlParameter();
                spIsActive.ParameterName = "@IsActive";
                if ((jobs.IsActive) != null)
                {
                    spIsActive.Value = jobs.IsActive;
                    spIsActive.SqlDbType = SqlDbType.Bit;
                }
                else
                {
                    spIsActive.Value = null;
                    spIsActive.SqlDbType = SqlDbType.Bit;
                }
                paramColl.Add(spIsActive);

              
                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramColl.Add(errorCount);


                #endregion
                if (flag == 1)
                {

                    GDSDBWrapper.ExecuteNonQueryOutParam("SP_PostNewJob", paramColl);
                }
                else
                {
                    SqlParameter spId = new SqlParameter();
                    spId.ParameterName = "@Id";
                    spId.Value = jobs.Id;
                    spId.SqlDbType = SqlDbType.Int;
                    paramColl.Add(spId);

                    GDSDBWrapper.ExecuteNonQueryOutParam("SP_UpdateJob", paramColl);
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }


        public int? PostNewEvent(Events events, int flag)
        {
            ArrayList paramColl = null;
            int? result = null;
            try
            {
                paramColl = new ArrayList();

                #region parameters


                SqlParameter spTitle = new SqlParameter();
                spTitle.ParameterName = "@Title";
                if (!string.IsNullOrEmpty(events.Title))
                {
                    spTitle.Value = events.Title.Trim();
                    spTitle.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spTitle.Value = null;
                    spTitle.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spTitle);


                SqlParameter spVenue = new SqlParameter();
                spVenue.ParameterName = "@Venue";
                if (!string.IsNullOrEmpty(events.Venue))
                {
                    spVenue.Value = events.Venue.Trim();
                    spVenue.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spVenue.Value = null;
                    spVenue.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spVenue);


                SqlParameter spDescription = new SqlParameter();
                spDescription.ParameterName = "@Description";
                if (!string.IsNullOrEmpty(events.Description))
                {
                    spDescription.Value = events.Description.Trim();
                    spDescription.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spDescription.Value = null;
                    spDescription.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spDescription);

                SqlParameter spDate = new SqlParameter();
                spDate.ParameterName = "@Date";
                if ((events.Date) != null)
                {
                    spDate.Value = events.Date;
                    spDate.SqlDbType = SqlDbType.Date;
                }
                else
                {
                    spDate.Value = null;
                    spDate.SqlDbType = SqlDbType.Date;
                }
                paramColl.Add(spDate);

                SqlParameter spStatus = new SqlParameter();
                spStatus.ParameterName = "@Status";
                if (events.Status)
                {
                    spStatus.Value = events.Status;
                    spStatus.SqlDbType = SqlDbType.Bit;
                }
                else
                {
                    spStatus.Value = false;
                    spStatus.SqlDbType = SqlDbType.Bit;
                }
                paramColl.Add(spStatus);



                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramColl.Add(errorCount);



                #endregion
                if (flag == 1)
                {

                    GDSDBWrapper.ExecuteNonQueryOutParam("SP_PostNewEvent", paramColl);
                    result = int.Parse(errorCount.Value.ToString());
                }
                else
                {
                    SqlParameter spId = new SqlParameter();
                    spId.ParameterName = "@Id";
                    spId.Value = events.Id;
                    spId.SqlDbType = SqlDbType.Int;
                    paramColl.Add(spId);

                    GDSDBWrapper.ExecuteNonQueryOutParam("SP_UpdateEvent", paramColl);
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }


        public int? PostNews(News news, int flag)
        {
            ArrayList paramColl = null;
            int? result = null;
            try
            {
                paramColl = new ArrayList();

                #region parameters


                SqlParameter spTitle = new SqlParameter();
                spTitle.ParameterName = "@Title";
                if (!string.IsNullOrEmpty(news.Title))
                {
                    spTitle.Value = news.Title.Trim();
                    spTitle.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spTitle.Value = null;
                    spTitle.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spTitle);



                SqlParameter spDescription = new SqlParameter();
                spDescription.ParameterName = "@Description";
                if (!string.IsNullOrEmpty(news.Description))
                {
                    spDescription.Value = news.Description.Trim();
                    spDescription.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spDescription.Value = null;
                    spDescription.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spDescription);



                SqlParameter spStatus = new SqlParameter();
                spStatus.ParameterName = "@Status";
                if (news.Status)
                {
                    spStatus.Value = news.Status;
                    spStatus.SqlDbType = SqlDbType.Bit;
                }
                else
                {
                    spStatus.Value = false;
                    spStatus.SqlDbType = SqlDbType.Bit;
                }
                paramColl.Add(spStatus);


                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramColl.Add(errorCount);



                #endregion
                if (flag == 1)
                {

                    GDSDBWrapper.ExecuteNonQueryOutParam("SP_PostNews", paramColl);

                }
                else
                {
                    SqlParameter spId = new SqlParameter();
                    spId.ParameterName = "@Id";
                    spId.Value = news.Id;
                    spId.SqlDbType = SqlDbType.Int;
                    paramColl.Add(spId);

                    GDSDBWrapper.ExecuteNonQueryOutParam("SP_UpdateNews", paramColl);
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }


        public int? InsertEventImages(EventImages eventImages)
        {
            ArrayList paramColl = null;
            int? result = null;
            try
            {
                paramColl = new ArrayList();

                #region parameters

                SqlParameter spId = new SqlParameter();
                spId.ParameterName = "@EventId";
                spId.Value = eventImages.Fk_EventId;
                spId.SqlDbType = SqlDbType.Int;
                paramColl.Add(spId);

                SqlParameter spImagePath = new SqlParameter();
                spImagePath.ParameterName = "@EventImagePath";
                spImagePath.Value = eventImages.ImageName;
                spImagePath.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(spImagePath);
                #endregion

                GDSDBWrapper.ExecuteNonQueryOutParam("SP_InsertEventImages", paramColl);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }

        public DataTable JobdetailsById(int? id)
        {
            DataTable JobDetailsById = new DataTable();
            ArrayList paramlist = null;
            try
            {
                paramlist = new ArrayList();

                SqlParameter jobid = new SqlParameter();
                jobid.ParameterName = "@id";
                if (id != null)
                {
                    jobid.Value = id;
                    jobid.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    jobid.Value = null;
                    jobid.SqlDbType = SqlDbType.Int;
                }


                paramlist.Add(jobid);


                JobDetailsById = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetJobDetails", paramlist);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return JobDetailsById;
        }


        public DataTable GetAppliedCandidateDetails(int? JobId)
        {
            DataTable AplliedjobDetails = new DataTable();
            ArrayList paramlist = null;
            try
            {
                paramlist = new ArrayList();

                SqlParameter jobid = new SqlParameter();
                jobid.ParameterName = "@JobId";
                if (JobId != null)
                {
                    jobid.Value = JobId;
                    jobid.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    jobid.Value = null;
                    jobid.SqlDbType = SqlDbType.Int;
                }


                paramlist.Add(jobid);


                AplliedjobDetails = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetAppliedCandidateDetails", paramlist);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return AplliedjobDetails;
        }

        public DataTable EventDetailsById(int? id)
        {
            DataTable JobDetailsById = new DataTable();
            ArrayList paramlist = null;
            try
            {
                paramlist = new ArrayList();

                SqlParameter jobid = new SqlParameter();
                jobid.ParameterName = "@id";
                if (id != null)
                {
                    jobid.Value = id;
                    jobid.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    jobid.Value = null;
                    jobid.SqlDbType = SqlDbType.Int;
                }


                paramlist.Add(jobid);


                JobDetailsById = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetEventDetailsById", paramlist);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return JobDetailsById;
        }

        public DataTable NewsDetailsById(int? id)
        {
            DataTable dt = new DataTable();
            ArrayList paramlist = null;
            try
            {
                paramlist = new ArrayList();

                SqlParameter jobid = new SqlParameter();
                jobid.ParameterName = "@Id";
                if (id != null)
                {
                    jobid.Value = id;
                    jobid.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    jobid.Value = null;
                    jobid.SqlDbType = SqlDbType.Int;
                }


                paramlist.Add(jobid);


                dt = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetNewsDetailsById", paramlist);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dt;
        }

        public DataTable GetDatesListinBetween(DateTime ExpenditureSDate, DateTime ExpenditureEDate)
        {
            DataTable dtDatesList = new DataTable();
            ArrayList paramlist = null;
            try
            {
                paramlist = new ArrayList();

                SqlParameter jobid = new SqlParameter();
                jobid.ParameterName = "@id";
                if (ExpenditureSDate != null)
                {
                    jobid.Value = ExpenditureSDate;
                    jobid.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    jobid.Value = null;
                    jobid.SqlDbType = SqlDbType.DateTime;
                }
                paramlist.Add(jobid);

                SqlParameter jobid1 = new SqlParameter();
                jobid1.ParameterName = "@id1";
                if (ExpenditureEDate != null)
                {
                    jobid1.Value = ExpenditureEDate;
                    jobid1.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    jobid1.Value = null;
                    jobid1.SqlDbType = SqlDbType.DateTime;
                }
                paramlist.Add(jobid1);


                dtDatesList = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetDaysInBetweenDates", paramlist);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return dtDatesList;
        }

        public int? CreateEmployeeAssets(EmployeeAssets objEmployeeAssets)
        {
            ArrayList paramColl = null;
            int? result = null;
            try
            {
                paramColl = new ArrayList();

                #region parameters

                #region FK_EmployeeID
                SqlParameter spFK_EmployeeID = new SqlParameter();
                spFK_EmployeeID.ParameterName = "@FK_EmployeeID";
                if (objEmployeeAssets.FK_EmployeeID != 0)
                {
                    spFK_EmployeeID.Value = objEmployeeAssets.FK_EmployeeID;
                    spFK_EmployeeID.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spFK_EmployeeID.Value = null;
                    spFK_EmployeeID.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spFK_EmployeeID);
                #endregion

                #region Laptop
                SqlParameter spLaptop = new SqlParameter();
                spLaptop.ParameterName = "@Laptop";
                spLaptop.Value = objEmployeeAssets.Laptop;
                spLaptop.SqlDbType = SqlDbType.Bit;
                paramColl.Add(spLaptop);
                #endregion

                #region LaptopNo
                SqlParameter spLaptopNumber = new SqlParameter();
                spLaptopNumber.ParameterName = "@LaptopNo";
                if (!(string.IsNullOrEmpty(objEmployeeAssets.LaptopNo)))
                {
                    spLaptopNumber.Value = objEmployeeAssets.LaptopNo;
                    spLaptopNumber.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spLaptopNumber.Value = null;
                    spLaptopNumber.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spLaptopNumber);
                #endregion

                #region LaptopBag
                SqlParameter spLaptopBag = new SqlParameter();
                spLaptopBag.ParameterName = "@LaptopBag";
                spLaptopBag.Value = objEmployeeAssets.LaptopBag;
                spLaptopBag.SqlDbType = SqlDbType.Bit;
                paramColl.Add(spLaptopBag);
                #endregion

                #region LaptopBagName
                SqlParameter spLaptopBagName = new SqlParameter();
                spLaptopBagName.ParameterName = "@LaptopBagName";
                if (!(string.IsNullOrEmpty(objEmployeeAssets.LaptopBagName)))
                {
                    spLaptopBagName.Value = objEmployeeAssets.LaptopBagName;
                    spLaptopBagName.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spLaptopBagName.Value = null;
                    spLaptopBagName.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spLaptopBagName);
                #endregion

                #region Datacard
                SqlParameter spDatacard = new SqlParameter();
                spDatacard.ParameterName = "@Datacard";
                spDatacard.Value = objEmployeeAssets.Datacard;
                spDatacard.SqlDbType = SqlDbType.Bit;
                paramColl.Add(spDatacard);
                #endregion

                #region DatacardNumber
                SqlParameter spDatacardNumber = new SqlParameter();
                spDatacardNumber.ParameterName = "@DatacardNo";
                if (!(string.IsNullOrEmpty(objEmployeeAssets.DatacardNo)))
                {
                    spDatacardNumber.Value = objEmployeeAssets.DatacardNo;
                    spDatacardNumber.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spDatacardNumber.Value = null;
                    spDatacardNumber.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spDatacardNumber);
                #endregion

                #region Simcard
                SqlParameter spSimcard = new SqlParameter();
                spSimcard.ParameterName = "@Simcard";
                spSimcard.Value = objEmployeeAssets.Simcard;
                spSimcard.SqlDbType = SqlDbType.Bit;
                paramColl.Add(spSimcard);
                #endregion

                #region SimcardNumber
                SqlParameter spSimcardNumber = new SqlParameter();
                spSimcardNumber.ParameterName = "@SimcardNo";
                if (!(string.IsNullOrEmpty(objEmployeeAssets.SimcardNo)))
                {
                    spSimcardNumber.Value = objEmployeeAssets.SimcardNo;
                    spSimcardNumber.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spSimcardNumber.Value = null;
                    spSimcardNumber.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spSimcardNumber);
                #endregion

                #region Cellphone
                SqlParameter spCellphone = new SqlParameter();
                spCellphone.ParameterName = "@Cellphone";
                spCellphone.Value = objEmployeeAssets.Cellphone;
                spCellphone.SqlDbType = SqlDbType.Bit;
                paramColl.Add(spCellphone);
                #endregion

                #region CellphoneNumber
                SqlParameter spCellphoneNumber = new SqlParameter();
                spCellphoneNumber.ParameterName = "@CellphoneNo";
                if (!(string.IsNullOrEmpty(objEmployeeAssets.CellphoneNo)))
                {
                    spCellphoneNumber.Value = objEmployeeAssets.CellphoneNo;
                    spCellphoneNumber.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spCellphoneNumber.Value = null;
                    spCellphoneNumber.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spCellphoneNumber);
                #endregion

                #region Multimeter
                SqlParameter spMultimeter = new SqlParameter();
                spMultimeter.ParameterName = "@Multimeter";
                spMultimeter.Value = objEmployeeAssets.Multimeter;
                spMultimeter.SqlDbType = SqlDbType.Bit;
                paramColl.Add(spMultimeter);
                #endregion

                #region MultimeterNumber
                SqlParameter spMultimeterNumber = new SqlParameter();
                spMultimeterNumber.ParameterName = "@MultimeterNo";
                if (!(string.IsNullOrEmpty(objEmployeeAssets.MultimeterNo)))
                {
                    spMultimeterNumber.Value = objEmployeeAssets.MultimeterNo;
                    spMultimeterNumber.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spMultimeterNumber.Value = null;
                    spMultimeterNumber.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spMultimeterNumber);
                #endregion

                #region Toolkit
                SqlParameter spToolkit = new SqlParameter();
                spToolkit.ParameterName = "@Toolkit";
                spToolkit.Value = objEmployeeAssets.Toolkit;
                spToolkit.SqlDbType = SqlDbType.Bit;
                paramColl.Add(spToolkit);
                #endregion

                #region ToolkitNumber
                SqlParameter spToolkitNumber = new SqlParameter();
                spToolkitNumber.ParameterName = "@ToolkitNo";
                if (!(string.IsNullOrEmpty(objEmployeeAssets.ToolkitNo)))
                {
                    spToolkitNumber.Value = objEmployeeAssets.ToolkitNo;
                    spToolkitNumber.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    spToolkitNumber.Value = null;
                    spToolkitNumber.SqlDbType = SqlDbType.NVarChar;
                }
                paramColl.Add(spToolkitNumber);
                #endregion

                #region Others
                SqlParameter spOthers = new SqlParameter();
                spOthers.ParameterName = "@Others";

                if (!(string.IsNullOrEmpty(objEmployeeAssets.Others)))
                {
                    spOthers.Value = objEmployeeAssets.Others;
                    spOthers.SqlDbType = SqlDbType.VarChar;
                }
                else
                {
                    spOthers.Value = null;
                    spOthers.SqlDbType = SqlDbType.VarChar;
                }
                paramColl.Add(spOthers);
                #endregion

                #endregion
                if (objEmployeeAssets.flag == 1)
                {
                    #region CreatedBy
                    SqlParameter spCreatedBy = new SqlParameter();
                    spCreatedBy.ParameterName = "@CreatedBy";

                    if (!(string.IsNullOrEmpty(objEmployeeAssets.CreatedBy)))
                    {
                        spCreatedBy.Value = objEmployeeAssets.CreatedBy;
                        spCreatedBy.SqlDbType = SqlDbType.VarChar;
                    }
                    else
                    {
                        spCreatedBy.Value = null;
                        spCreatedBy.SqlDbType = SqlDbType.VarChar;
                    }
                    paramColl.Add(spCreatedBy);
                    #endregion

                    GDSDBWrapper.ExecuteNonQueryOutParam("SP_CreateEmployeeAssets", paramColl);
                }
                else
                {
                    #region ModifiedBy
                    SqlParameter spCreatedBy = new SqlParameter();
                    spCreatedBy.ParameterName = "@ModifiedBy";

                    if (!(string.IsNullOrEmpty(objEmployeeAssets.CreatedBy)))
                    {
                        spCreatedBy.Value = objEmployeeAssets.CreatedBy;
                        spCreatedBy.SqlDbType = SqlDbType.VarChar;
                    }
                    else
                    {
                        spCreatedBy.Value = null;
                        spCreatedBy.SqlDbType = SqlDbType.VarChar;
                    }
                    paramColl.Add(spCreatedBy);
                    #endregion

                    SqlParameter spId = new SqlParameter();
                    spId.ParameterName = "@Id";
                    spId.Value = objEmployeeAssets.Id;
                    spId.SqlDbType = SqlDbType.Int;
                    paramColl.Add(spId);

                    GDSDBWrapper.ExecuteNonQueryOutParam("SP_UpdateEmployeeAssets", paramColl);
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }

        public DataTable GetEmployeeAssets()
        {
            DataTable dtEmployeeAssets = new DataTable();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();
                dtEmployeeAssets = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetEmployeeAssets", paramColl);
            }
            catch (Exception EX)
            {
                throw EX;
            }

            return dtEmployeeAssets;
        }

        public DataTable GetSkillMetrics(string Employeeid)
        {
            DataTable dtSkillmetrics = new DataTable();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();
                SqlParameter sqlEmpid = new SqlParameter();
                sqlEmpid.ParameterName = "@Employeeid";
                sqlEmpid.Value = Employeeid;
                sqlEmpid.SqlDbType = SqlDbType.VarChar;
                paramColl.Add(sqlEmpid);

                dtSkillmetrics = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetSkillMetrics", paramColl);

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return dtSkillmetrics;
        }
        public DataTable ValidateUserForSkill(string empid)
        {
            ArrayList paramColl = null;
            DataTable SalaryDetailsById = new DataTable();
            try
            {
                paramColl = new ArrayList();
                if (!string.IsNullOrEmpty(empid))
                {
                    SqlParameter sqlEmpid = new SqlParameter();
                    sqlEmpid.ParameterName = "@Empid";
                    sqlEmpid.Value = empid;
                    sqlEmpid.SqlDbType = SqlDbType.NVarChar;
                    paramColl.Add(sqlEmpid);
                }
                SalaryDetailsById = GDSDBWrapper.ExecuteDataReaderMultiParam("sp_ValidateUserforSkill", paramColl);
            }
            catch
            {
            }
            return SalaryDetailsById;
        }
        public int? InsertSkillMetrics(SkillsMetrics skillsMetrics, int var)
        {
            ArrayList paramColl = null;
            int? result = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter sqlEmpid = new SqlParameter();
                sqlEmpid.ParameterName = "@Empid";
                sqlEmpid.Value = skillsMetrics.Empid;
                sqlEmpid.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlEmpid);

                SqlParameter sqlGC = new SqlParameter();
                sqlGC.ParameterName = "@GC";
                sqlGC.Value = skillsMetrics.GC;
                sqlGC.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlGC);

                SqlParameter sqlHSS = new SqlParameter();
                sqlHSS.ParameterName = "@HSS";
                sqlHSS.Value = skillsMetrics.HSS;
                sqlHSS.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlHSS);

                SqlParameter sqlASS = new SqlParameter();
                sqlASS.ParameterName = "@ASS";
                sqlASS.Value = skillsMetrics.ASS;
                sqlASS.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlASS);

                SqlParameter sqlICPOES = new SqlParameter();
                sqlICPOES.ParameterName = "@ICPOES";
                sqlICPOES.Value = skillsMetrics.ICPOES;
                sqlICPOES.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlICPOES);

                SqlParameter sqlCTC = new SqlParameter();
                sqlCTC.ParameterName = "@CTC";
                sqlCTC.Value = skillsMetrics.CTC;
                sqlCTC.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlCTC);

                SqlParameter sqlHPLC = new SqlParameter();
                sqlHPLC.ParameterName = "@HPLC";
                sqlHPLC.Value = skillsMetrics.HPLC;
                sqlHPLC.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlHPLC);

                SqlParameter sqlInfinity = new SqlParameter();
                sqlInfinity.ParameterName = "@Infinity";
                sqlInfinity.Value = skillsMetrics.Infinity;
                sqlInfinity.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlInfinity);

                SqlParameter sqlRRLC = new SqlParameter();
                sqlRRLC.ParameterName = "@RRLC";
                sqlRRLC.Value = skillsMetrics.RRLC;
                sqlRRLC.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlRRLC);

                SqlParameter sqlACE = new SqlParameter();
                sqlACE.ParameterName = "@ACE";
                sqlACE.Value = skillsMetrics.ACE;
                sqlACE.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlACE);

                SqlParameter sqlEzchrom = new SqlParameter();
                sqlEzchrom.ParameterName = "@Ezchrom";
                sqlEzchrom.Value = skillsMetrics.Ezchrom;
                sqlEzchrom.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlEzchrom);

                SqlParameter sqlELSD = new SqlParameter();
                sqlELSD.ParameterName = "@ELSD";
                sqlELSD.Value = skillsMetrics.ELSD;
                sqlELSD.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlELSD);

                SqlParameter sqlGCMSD = new SqlParameter();
                sqlGCMSD.ParameterName = "@GCMSD";
                sqlGCMSD.Value = skillsMetrics.GCMSD;
                sqlGCMSD.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlGCMSD);

                SqlParameter sqlGCMSorMS = new SqlParameter();
                sqlGCMSorMS.ParameterName = "@GCMSorMS";
                sqlGCMSorMS.Value = skillsMetrics.GCMSorMS;
                sqlGCMSorMS.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlGCMSorMS);

                SqlParameter sqlICPMS = new SqlParameter();
                sqlICPMS.ParameterName = "@ICPMS";
                sqlICPMS.Value = skillsMetrics.ICPMS;
                sqlICPMS.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlICPMS);

                SqlParameter sqlLCMSSQ = new SqlParameter();
                sqlLCMSSQ.ParameterName = "@LCMSSQ";
                sqlLCMSSQ.Value = skillsMetrics.LCMSSQ;
                sqlLCMSSQ.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlLCMSSQ);

                SqlParameter sqlLCMSQQQ = new SqlParameter();
                sqlLCMSQQQ.ParameterName = "@LCMSQQQ";
                sqlLCMSQQQ.Value = skillsMetrics.LCMSQQQ;
                sqlLCMSQQQ.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlLCMSQQQ);

                SqlParameter sqlLCMSTrap = new SqlParameter();
                sqlLCMSTrap.ParameterName = "@LCMSTrap";
                sqlLCMSTrap.Value = skillsMetrics.LCMSTrap;
                sqlLCMSTrap.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlLCMSTrap);

                SqlParameter sqlCE = new SqlParameter();
                sqlCE.ParameterName = "@CE";
                sqlCE.Value = skillsMetrics.CE;
                sqlCE.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlCE);

                SqlParameter sqlOpenLab = new SqlParameter();
                sqlOpenLab.ParameterName = "@OpenLab";
                sqlOpenLab.Value = skillsMetrics.OpenLab;
                sqlOpenLab.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlOpenLab);

                SqlParameter sqlGasSamplingValves = new SqlParameter();
                sqlGasSamplingValves.ParameterName = "@GasSamplingValves";
                sqlGasSamplingValves.Value = skillsMetrics.GasSamplingValves;
                sqlGasSamplingValves.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlGasSamplingValves);

                SqlParameter sqlAnyOtherSkill = new SqlParameter();
                sqlAnyOtherSkill.ParameterName = "@AnyOtherSkill";
                sqlAnyOtherSkill.Value = skillsMetrics.AnyOtherSkill;
                sqlAnyOtherSkill.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlAnyOtherSkill);

                SqlParameter sqlSkill1 = new SqlParameter();
                sqlSkill1.ParameterName = "@Skill1";
                sqlSkill1.Value = skillsMetrics.Skill1;
                sqlSkill1.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlSkill1);

                SqlParameter sqlSkill2 = new SqlParameter();
                sqlSkill2.ParameterName = "@Skill2";
                sqlSkill2.Value = skillsMetrics.Skill2;
                sqlSkill2.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlSkill2);

                SqlParameter sqlSkill3 = new SqlParameter();
                sqlSkill3.ParameterName = "@Skill3";
                sqlSkill3.Value = skillsMetrics.Skill3;
                sqlSkill3.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlSkill3);

                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramColl.Add(errorCount);

                if (var == 1)
                {
                    GDSDBWrapper.ExecuteNonQueryOutParam("SP_InsertSkillMetrics", paramColl);
                }
                else
                {
                    GDSDBWrapper.ExecuteNonQueryOutParam("SP_UpdateSkillMetrics", paramColl);
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }
        public int? InsertSalaryDetails(SalaryDetails salaryDetails, int? flag)
        {
            ArrayList paramColl = null;
            int? result = null;
            try
            {
                paramColl = new ArrayList();

                //SqlParameter sqlId = new SqlParameter();
                //sqlId.ParameterName = "@Id";
                //sqlId.Value = salaryDetails.Id;
                //sqlId.SqlDbType = SqlDbType.Int;
                //paramColl.Add(sqlId);

                SqlParameter sqlEmpcode = new SqlParameter();
                sqlEmpcode.ParameterName = "@EmployeeID";
                sqlEmpcode.Value = salaryDetails.Empcode.Trim();
                sqlEmpcode.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlEmpcode);

                SqlParameter sqlEmployeeName = new SqlParameter();
                sqlEmployeeName.ParameterName = "@EmployeeName";
                sqlEmployeeName.Value = salaryDetails.EmployeeName;
                sqlEmployeeName.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlEmployeeName);

                SqlParameter sqlDesignation = new SqlParameter();
                sqlDesignation.ParameterName = "@Designation";
                sqlDesignation.Value = salaryDetails.Designation;
                sqlDesignation.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlDesignation);


                SqlParameter sqlLocation = new SqlParameter();
                sqlLocation.ParameterName = "@Location";
                sqlLocation.Value = salaryDetails.Location;
                sqlLocation.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlLocation);

                SqlParameter sqlDOB = new SqlParameter();
                sqlDOB.ParameterName = "@DOB";
                sqlDOB.Value = salaryDetails.DOB;
                sqlDOB.SqlDbType = SqlDbType.DateTime;
                paramColl.Add(sqlDOB);

                SqlParameter sqlDOJ = new SqlParameter();
                sqlDOJ.ParameterName = "@DOJ";
                sqlDOJ.Value = salaryDetails.DOJ;
                sqlDOJ.SqlDbType = SqlDbType.DateTime;
                paramColl.Add(sqlDOJ);

                SqlParameter sqlPAN = new SqlParameter();
                sqlPAN.ParameterName = "@PAN";
                sqlPAN.Value = salaryDetails.PAN;
                sqlPAN.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlPAN);

                SqlParameter sqlPFHash = new SqlParameter();
                sqlPFHash.ParameterName = "@PFHash";
                sqlPFHash.Value = salaryDetails.PFHash;
                sqlPFHash.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlPFHash);

                SqlParameter sqlUANHash = new SqlParameter();
                sqlUANHash.ParameterName = "@UANHash";
                sqlUANHash.Value = salaryDetails.UANHash;
                sqlUANHash.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlUANHash);

                SqlParameter sqlESIHash = new SqlParameter();
                sqlESIHash.ParameterName = "@ESIHash";
                sqlESIHash.Value = salaryDetails.ESIHash;
                sqlESIHash.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlESIHash);

                SqlParameter sqlWD = new SqlParameter();
                sqlWD.ParameterName = "@WD";
                sqlWD.Value = salaryDetails.WD;
                sqlWD.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlWD);

                SqlParameter sqlPD = new SqlParameter();
                sqlPD.ParameterName = "@PD";
                sqlPD.Value = salaryDetails.PD;
                sqlPD.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlPD);

                SqlParameter sqlCL = new SqlParameter();
                sqlCL.ParameterName = "@CL";
                sqlCL.Value = salaryDetails.CL;
                sqlCL.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlCL);

                SqlParameter sqlSL = new SqlParameter();
                sqlSL.ParameterName = "@SL";
                sqlSL.Value = salaryDetails.SL;
                sqlSL.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlSL);

                SqlParameter sqlCLR = new SqlParameter();
                sqlCLR.ParameterName = "@CLR";
                sqlCLR.Value = salaryDetails.CLR;
                sqlCLR.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlCLR);

                SqlParameter sqlSLR = new SqlParameter();
                sqlSLR.ParameterName = "@SLR";
                sqlSLR.Value = salaryDetails.SLR;
                sqlSLR.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlSLR);

                SqlParameter sqlBasic = new SqlParameter();
                sqlBasic.ParameterName = "@Basic";
                sqlBasic.Value = salaryDetails.Basic;
                sqlBasic.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlBasic);

                SqlParameter sqlBasicEarned = new SqlParameter();
                sqlBasicEarned.ParameterName = "@BasicEarned";
                sqlBasicEarned.Value = salaryDetails.BasicEarned;
                sqlBasicEarned.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlBasicEarned);

                SqlParameter sqlHRA = new SqlParameter();
                sqlHRA.ParameterName = "@HRA ";
                sqlHRA.Value = salaryDetails.HRA;
                sqlHRA.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlHRA);

                SqlParameter sqlHRAEarned = new SqlParameter();
                sqlHRAEarned.ParameterName = "@HRAEarned ";
                sqlHRAEarned.Value = salaryDetails.HRAEarned;
                sqlHRAEarned.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlHRAEarned);

                SqlParameter sqlConv = new SqlParameter();
                sqlConv.ParameterName = "@Conv ";
                sqlConv.Value = salaryDetails.Conv;
                sqlConv.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlConv);

                SqlParameter sqlConvEarned = new SqlParameter();
                sqlConvEarned.ParameterName = "@ConvEarned";
                sqlConvEarned.Value = salaryDetails.ConvEarned;
                sqlConvEarned.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlConvEarned);

                SqlParameter sqlSplAlw = new SqlParameter();
                sqlSplAlw.ParameterName = "@SplAlw";
                sqlSplAlw.Value = salaryDetails.SplAlw;
                sqlSplAlw.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlSplAlw);

                SqlParameter sqlSplAlEarned = new SqlParameter();
                sqlSplAlEarned.ParameterName = "@SplAlEarned";
                sqlSplAlEarned.Value = salaryDetails.SplAlEarned;
                sqlSplAlEarned.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlSplAlEarned);

                SqlParameter sqlVariablepay = new SqlParameter();
                sqlVariablepay.ParameterName = "@Variablepay";
                sqlVariablepay.Value = salaryDetails.Variablepay;
                sqlVariablepay.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlVariablepay);

                SqlParameter sqlVariablepayearned = new SqlParameter();
                sqlVariablepayearned.ParameterName = "@Variablepayearned";
                sqlVariablepayearned.Value = salaryDetails.Variablepayearned;
                sqlVariablepayearned.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlVariablepayearned);

                SqlParameter sqlGrossPay = new SqlParameter();
                sqlGrossPay.ParameterName = "@GrossPay";
                sqlGrossPay.Value = salaryDetails.GrossPay;
                sqlGrossPay.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlGrossPay);

                SqlParameter sqlGrossEarned = new SqlParameter();
                sqlGrossEarned.ParameterName = "@GrossEarned";
                sqlGrossEarned.Value = salaryDetails.GrossEarned;
                sqlGrossEarned.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlGrossEarned);

                SqlParameter sqlPF = new SqlParameter();
                sqlPF.ParameterName = "@PF";
                sqlPF.Value = salaryDetails.PF;
                sqlPF.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlPF);

                SqlParameter sqlPFDeducted = new SqlParameter();
                sqlPFDeducted.ParameterName = "@PFDeducted";
                sqlPFDeducted.Value = salaryDetails.PFDeducted;
                sqlPFDeducted.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlPFDeducted);

                SqlParameter sqlESI = new SqlParameter();
                sqlESI.ParameterName = "@ESI";
                sqlESI.Value = salaryDetails.ESI;
                sqlESI.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlESI);

                SqlParameter sqlESIDeducted = new SqlParameter();
                sqlESIDeducted.ParameterName = "@ESIDeducted";
                sqlESIDeducted.Value = salaryDetails.ESIDeducted;
                sqlESIDeducted.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlESIDeducted);


                SqlParameter sqlPT = new SqlParameter();
                sqlPT.ParameterName = "@PT";
                sqlPT.Value = salaryDetails.PT;
                sqlPT.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlPT);

                SqlParameter sqlTDS = new SqlParameter();
                sqlTDS.ParameterName = "@TDS";
                sqlTDS.Value = salaryDetails.TDS;
                sqlTDS.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlTDS);

                SqlParameter sqlothers = new SqlParameter();
                sqlothers.ParameterName = "@others";
                sqlothers.Value = salaryDetails.others;
                sqlothers.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlothers);

                SqlParameter sqlNetPay = new SqlParameter();
                sqlNetPay.ParameterName = "@NetPay";
                sqlNetPay.Value = salaryDetails.NetPay;
                sqlNetPay.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlNetPay);

                SqlParameter sqlNetPaid = new SqlParameter();
                sqlNetPaid.ParameterName = "@NetPaid";
                sqlNetPaid.Value = salaryDetails.NetPaid;
                sqlNetPaid.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlNetPaid);

                SqlParameter sqlNetPiadforACC = new SqlParameter();
                sqlNetPiadforACC.ParameterName = "@NetPiadforACC";
                sqlNetPiadforACC.Value = salaryDetails.NetPiadforACC;
                sqlNetPiadforACC.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlNetPiadforACC);

                SqlParameter sqlPFEmployer = new SqlParameter();
                sqlPFEmployer.ParameterName = "@PFEmployer";
                sqlPFEmployer.Value = salaryDetails.PFEmployer;
                sqlPFEmployer.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlPFEmployer);

                SqlParameter sqlPFEmployerPaid = new SqlParameter();
                sqlPFEmployerPaid.ParameterName = "@PFEmployerPaid";
                sqlPFEmployerPaid.Value = salaryDetails.PFEmployerPaid;
                sqlPFEmployerPaid.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlPFEmployerPaid);

                SqlParameter sqlESIEmployer = new SqlParameter();
                sqlESIEmployer.ParameterName = "@ESIEmployer";
                sqlESIEmployer.Value = salaryDetails.ESIEmployer;
                sqlESIEmployer.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlESIEmployer);

                SqlParameter sqlESIEmplyerPaid = new SqlParameter();
                sqlESIEmplyerPaid.ParameterName = "@ESIEmplyerPaid";
                sqlESIEmplyerPaid.Value = salaryDetails.ESIEmplyerPaid;
                sqlESIEmplyerPaid.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlESIEmplyerPaid);

                SqlParameter sqlMonth = new SqlParameter();
                sqlMonth.ParameterName = "@Month";
                sqlMonth.Value = salaryDetails.Month;
                sqlMonth.SqlDbType = SqlDbType.DateTime;
                paramColl.Add(sqlMonth);

                SqlParameter sqlCTC = new SqlParameter();
                sqlCTC.ParameterName = "@CTCMonth";
                sqlCTC.Value = salaryDetails.CTCMonth;
                sqlCTC.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlCTC);


                SqlParameter sqlBank = new SqlParameter();
                sqlBank.ParameterName = "@Bank";
                sqlBank.Value = salaryDetails.Bank;
                sqlBank.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlBank);


                SqlParameter sqlAccount = new SqlParameter();
                sqlAccount.ParameterName = "@Account";
                sqlAccount.Value = salaryDetails.Account;
                sqlAccount.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlAccount);

                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramColl.Add(errorCount);
                if (flag == 1)
                    GDSDBWrapper.ExecuteNonQueryOutParam("SP_InsertSalaryDetails", paramColl);
                else
                    GDSDBWrapper.ExecuteNonQueryOutParam("SP_UpdateSalaryDetails", paramColl);


            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }




        public int? InsertSalaryDetailsNewFormat(SalaryDetails_NEW salaryDetails, int? flag)
        {
            ArrayList paramColl = null;
            int? result = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter sqlEmpcode = new SqlParameter();
                sqlEmpcode.ParameterName = "@EmployeeID";
                sqlEmpcode.Value = salaryDetails.Empcode.Trim();
                sqlEmpcode.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlEmpcode);

                SqlParameter sqlEmployeeName = new SqlParameter();
                sqlEmployeeName.ParameterName = "@EmployeeName";
                sqlEmployeeName.Value = salaryDetails.EmployeeName.Trim();
                sqlEmployeeName.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlEmployeeName);

                SqlParameter sqlDesignation = new SqlParameter();
                sqlDesignation.ParameterName = "@Designation";
                sqlDesignation.Value = salaryDetails.Designation;
                sqlDesignation.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlDesignation);

                SqlParameter sqlLevel = new SqlParameter();
                sqlLevel.ParameterName = "@Level";
                sqlLevel.Value = salaryDetails.Level;
                sqlLevel.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlLevel);

                SqlParameter sqlLocation = new SqlParameter();
                sqlLocation.ParameterName = "@Location";
                sqlLocation.Value = salaryDetails.Location;
                sqlLocation.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlLocation);

                SqlParameter sqlDOB = new SqlParameter();
                sqlDOB.ParameterName = "@DOB";
                sqlDOB.Value = salaryDetails.DOB;
                sqlDOB.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlDOB);

                SqlParameter sqlDOJ = new SqlParameter();
                sqlDOJ.ParameterName = "@DOJ";
                sqlDOJ.Value = salaryDetails.DOJ;
                sqlDOJ.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlDOJ);

                SqlParameter sqlPAN = new SqlParameter();
                sqlPAN.ParameterName = "@PAN";
                sqlPAN.Value = salaryDetails.PAN;
                sqlPAN.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlPAN);

                SqlParameter sqlPFHash = new SqlParameter();
                sqlPFHash.ParameterName = "@PFHash";
                sqlPFHash.Value = salaryDetails.PFHash;
                sqlPFHash.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlPFHash);

                SqlParameter sqlUANHash = new SqlParameter();
                sqlUANHash.ParameterName = "@UANHash";
                sqlUANHash.Value = salaryDetails.UANHash;
                sqlUANHash.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlUANHash);

                SqlParameter sqlESIHash = new SqlParameter();
                sqlESIHash.ParameterName = "@ESIHash";
                sqlESIHash.Value = salaryDetails.ESIHash;
                sqlESIHash.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlESIHash);

                SqlParameter sqlWD = new SqlParameter();
                sqlWD.ParameterName = "@WD";
                sqlWD.Value = salaryDetails.WD;
                sqlWD.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlWD);

                SqlParameter sqlPD = new SqlParameter();
                sqlPD.ParameterName = "@PD";
                sqlPD.Value = salaryDetails.PD;
                sqlPD.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlPD);

                SqlParameter sqlCL = new SqlParameter();
                sqlCL.ParameterName = "@CL";
                sqlCL.Value = salaryDetails.CL;
                sqlCL.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlCL);

                SqlParameter sqlSL = new SqlParameter();
                sqlSL.ParameterName = "@SL";
                sqlSL.Value = salaryDetails.SL;
                sqlSL.SqlDbType = SqlDbType.Decimal;
                paramColl.Add(sqlSL);

                SqlParameter sqlMonth1 = new SqlParameter();
                sqlMonth1.ParameterName = "@Month1";
                sqlMonth1.Value = salaryDetails.Month1;
                sqlMonth1.SqlDbType = SqlDbType.DateTime;
                paramColl.Add(sqlMonth1);

                ////////////////////////////////////

                SqlParameter sqlBasicMonthly = new SqlParameter();
                sqlBasicMonthly.ParameterName = "@BasicMonthly";
                sqlBasicMonthly.Value = salaryDetails.BasicMonthly;
                sqlBasicMonthly.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlBasicMonthly);

                SqlParameter sqlBasicPaid = new SqlParameter();
                sqlBasicPaid.ParameterName = "@BasicPaid";
                sqlBasicPaid.Value = salaryDetails.BasicPaid;
                sqlBasicPaid.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlBasicPaid);

                SqlParameter sqlBasicProjected = new SqlParameter();
                sqlBasicProjected.ParameterName = "@BasicProjected";
                sqlBasicProjected.Value = salaryDetails.BasicProjected;
                sqlBasicProjected.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlBasicProjected);

                SqlParameter sqlHRAmonthly = new SqlParameter();
                sqlHRAmonthly.ParameterName = "@HRAmonthly";
                sqlHRAmonthly.Value = salaryDetails.HRAmonthly;
                sqlHRAmonthly.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlHRAmonthly);

                SqlParameter sqlHRAPaid = new SqlParameter();
                sqlHRAPaid.ParameterName = "@HRAPaid";
                sqlHRAPaid.Value = salaryDetails.HRAPaid;
                sqlHRAPaid.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlHRAPaid);

                SqlParameter sqlHRAProjected = new SqlParameter();
                sqlHRAProjected.ParameterName = "@HRAProjected";
                sqlHRAProjected.Value = salaryDetails.HRAProjected;
                sqlHRAProjected.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlHRAProjected);

                SqlParameter sqlConvMonthly = new SqlParameter();
                sqlConvMonthly.ParameterName = "@ConvMonthly";
                sqlConvMonthly.Value = salaryDetails.ConvMonthly;
                sqlConvMonthly.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlConvMonthly);


                SqlParameter sqlConvPaid = new SqlParameter();
                sqlConvPaid.ParameterName = "@ConvPaid";
                sqlConvPaid.Value = salaryDetails.ConvPaid;
                sqlConvPaid.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlConvPaid);



                SqlParameter sqlConvProjected = new SqlParameter();
                sqlConvProjected.ParameterName = "@ConvProjected";
                sqlConvProjected.Value = salaryDetails.ConvProjected;
                sqlConvProjected.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlConvProjected);

                SqlParameter sqlSplAlwMonthly = new SqlParameter();
                sqlSplAlwMonthly.ParameterName = "@SplAlwMonthly";
                sqlSplAlwMonthly.Value = salaryDetails.SplAlwMonthly;
                sqlSplAlwMonthly.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlSplAlwMonthly);

                SqlParameter sqlSplAlwPaid = new SqlParameter();
                sqlSplAlwPaid.ParameterName = "@SplAlwPaid";
                sqlSplAlwPaid.Value = salaryDetails.SplAlwPaid;
                sqlSplAlwPaid.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlSplAlwPaid);



                SqlParameter sqlSplAlwProjected = new SqlParameter();
                sqlSplAlwProjected.ParameterName = "@SplAlwProjected";
                sqlSplAlwProjected.Value = salaryDetails.SplAlwProjected;
                sqlSplAlwProjected.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlSplAlwProjected);

                SqlParameter sqlGrossMonthly = new SqlParameter();
                sqlGrossMonthly.ParameterName = "@GrossMonthly";
                sqlGrossMonthly.Value = salaryDetails.GrossMonthly;
                sqlGrossMonthly.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlGrossMonthly);

                SqlParameter sqlNetSalaryMonthly = new SqlParameter();
                sqlNetSalaryMonthly.ParameterName = "@NetSalaryMonthly";
                sqlNetSalaryMonthly.Value = salaryDetails.NetSalaryMonthly;
                sqlNetSalaryMonthly.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlNetSalaryMonthly);

                SqlParameter sqlPFEmployerMonthly = new SqlParameter();
                sqlPFEmployerMonthly.ParameterName = "@PFEmployerMonthly";
                sqlPFEmployerMonthly.Value = salaryDetails.PFEmployerMonthly;
                sqlPFEmployerMonthly.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlPFEmployerMonthly);

                SqlParameter sqlESIEmployerMonthly = new SqlParameter();
                sqlESIEmployerMonthly.ParameterName = "@ESIEmployerMonthly";
                sqlESIEmployerMonthly.Value = salaryDetails.ConvPaid;
                sqlESIEmployerMonthly.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlESIEmployerMonthly);

                SqlParameter sqlPT = new SqlParameter();
                sqlPT.ParameterName = "@PT";
                sqlPT.Value = salaryDetails.PT;
                sqlPT.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlPT);





                SqlParameter sqlTDS = new SqlParameter();
                sqlTDS.ParameterName = "@TDS";
                sqlTDS.Value = salaryDetails.TDS;
                sqlTDS.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlTDS);

                SqlParameter sqlTotalDeduct = new SqlParameter();
                sqlTotalDeduct.ParameterName = "@TotalDeduct";
                sqlTotalDeduct.Value = salaryDetails.TotalDeduct;
                sqlTotalDeduct.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlTotalDeduct);

                SqlParameter sqlPFEmployerP = new SqlParameter();
                sqlPFEmployerP.ParameterName = "@PFEmployerP";
                sqlPFEmployerP.Value = salaryDetails.PFEmployerP;
                sqlPFEmployerP.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlPFEmployerP);

                SqlParameter sqlESIEmployerP = new SqlParameter();
                sqlESIEmployerP.ParameterName = "@ESIEmployerP";
                sqlESIEmployerP.Value = salaryDetails.ESIEmployerP;
                sqlESIEmployerP.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlESIEmployerP);

                SqlParameter sqlMedicalAllowanceYearly = new SqlParameter();
                sqlMedicalAllowanceYearly.ParameterName = "@MedicalAllowanceYearly";
                sqlMedicalAllowanceYearly.Value = salaryDetails.MedicalAllowanceYearly;
                sqlMedicalAllowanceYearly.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlMedicalAllowanceYearly);

                SqlParameter sqlLTAYearly = new SqlParameter();
                sqlLTAYearly.ParameterName = "@LTAYearly";
                sqlLTAYearly.Value = salaryDetails.LTAYearly;
                sqlLTAYearly.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlLTAYearly);

                SqlParameter sqlPerformancebasedVariablepayYearly = new SqlParameter();
                sqlPerformancebasedVariablepayYearly.ParameterName = "@PerformancebasedVariablepayYearly";
                sqlPerformancebasedVariablepayYearly.Value = salaryDetails.PerformancebasedVariablepayYearly;
                sqlPerformancebasedVariablepayYearly.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlPerformancebasedVariablepayYearly);

                SqlParameter sqlTotalProjected = new SqlParameter();
                sqlTotalProjected.ParameterName = "@TotalProjected";
                sqlTotalProjected.Value = salaryDetails.TotalProjected;
                sqlTotalProjected.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlTotalProjected);

                SqlParameter sqlHealthInsurance = new SqlParameter();
                sqlHealthInsurance.ParameterName = "@HealthInsurance";
                sqlHealthInsurance.Value = salaryDetails.HealthInsurance;
                sqlHealthInsurance.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlHealthInsurance);

                SqlParameter sqlGroupInsurance = new SqlParameter();
                sqlGroupInsurance.ParameterName = "@GroupInsurance";
                sqlGroupInsurance.Value = salaryDetails.GroupInsurance;
                sqlGroupInsurance.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlGroupInsurance);

                SqlParameter sqlPFEmployerPaid = new SqlParameter();
                sqlPFEmployerPaid.ParameterName = "@PFEmployerPaid";
                sqlPFEmployerPaid.Value = salaryDetails.PFEmployerPaid;
                sqlPFEmployerPaid.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlPFEmployerPaid);

                SqlParameter sqlESIEmployerPaid = new SqlParameter();
                sqlESIEmployerPaid.ParameterName = "@ESIEmployerPaid";
                sqlESIEmployerPaid.Value = salaryDetails.ESIEmployerPaid;
                sqlPT.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlESIEmployerPaid);

                SqlParameter sqlMedicalAllowancePaid = new SqlParameter();
                sqlMedicalAllowancePaid.ParameterName = "@MedicalAllowancePaid";
                sqlMedicalAllowancePaid.Value = salaryDetails.MedicalAllowancePaid;
                sqlMedicalAllowancePaid.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlMedicalAllowancePaid);

                SqlParameter sqlLTAPaid = new SqlParameter();
                sqlLTAPaid.ParameterName = "@LTAPaid";
                sqlLTAPaid.Value = salaryDetails.LTAPaid;
                sqlLTAPaid.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlLTAPaid);

                SqlParameter sqlSoftskillsYearly = new SqlParameter();
                sqlSoftskillsYearly.ParameterName = "@SoftskillsYearly";
                sqlSoftskillsYearly.Value = salaryDetails.SoftskillsYearly;
                sqlSoftskillsYearly.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlSoftskillsYearly);

                SqlParameter sqlTravelAllowancePaid = new SqlParameter();
                sqlTravelAllowancePaid.ParameterName = "@TravelAllowancePaid";
                sqlTravelAllowancePaid.Value = salaryDetails.TravelAllowancePaid;
                sqlTravelAllowancePaid.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlTravelAllowancePaid);

                SqlParameter sqlTotalPaid = new SqlParameter();
                sqlTotalPaid.ParameterName = "@TotalPaid";
                sqlTotalPaid.Value = salaryDetails.TotalPaid;
                sqlTotalPaid.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlTotalPaid);


                SqlParameter sqlPFEmployeeMonthly = new SqlParameter();
                sqlPFEmployeeMonthly.ParameterName = "@PFEmployeeMonthly";
                sqlPFEmployeeMonthly.Value = salaryDetails.PFEmployeeMonthly;
                sqlPFEmployeeMonthly.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlPFEmployeeMonthly);

                SqlParameter sqlESIEmployeeMonthly = new SqlParameter();
                sqlESIEmployeeMonthly.ParameterName = "@ESIEmployeeMonthly";
                sqlESIEmployeeMonthly.Value = salaryDetails.ESIEmployeeMonthly;
                sqlESIEmployeeMonthly.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(sqlESIEmployeeMonthly);
                
                ////////////////////////////////////
                SqlParameter errorCount = new SqlParameter();
                errorCount.ParameterName = "@iError";
                errorCount.SqlDbType = SqlDbType.Int;
                errorCount.Direction = ParameterDirection.Output;
                paramColl.Add(errorCount);
                if (flag == 1)
                    GDSDBWrapper.ExecuteNonQueryOutParam("SP_InsertSalaryDetailsNewFormat", paramColl);
                else
                    GDSDBWrapper.ExecuteNonQueryOutParam("SP_UpdateSalaryDetailsNewFormat", paramColl);


            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }
        public DataTable GetSalaryDetails(string EmployeeId)
        {
            DataTable SalaryDetailsById = new DataTable();
            ArrayList paramlist = null;
            try
            {
                paramlist = new ArrayList();
                
                SqlParameter sqlEmpid = new SqlParameter();
                sqlEmpid.ParameterName = "@Employeeid";
                sqlEmpid.Value = EmployeeId;
                sqlEmpid.SqlDbType = SqlDbType.NVarChar;
                paramlist.Add(sqlEmpid);
              
                SalaryDetailsById = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetSalaryDetails", paramlist);
             
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return SalaryDetailsById;
        }
        public DataTable EmpGetSalaryDetailsForMonth(string EmployeeId,int month)
        {
            DataTable SalaryDetailsById = new DataTable();
            ArrayList paramlist = null;
            try
            {
                paramlist = new ArrayList();

                SqlParameter sqlEmpid = new SqlParameter();
                sqlEmpid.ParameterName = "@Employeeid";
                sqlEmpid.Value = EmployeeId.Trim();
                sqlEmpid.SqlDbType = SqlDbType.NVarChar;
                paramlist.Add(sqlEmpid);


                SqlParameter sqlMonth = new SqlParameter();
                sqlMonth.ParameterName = "@Month";
                sqlMonth.Value = month;
                sqlMonth.SqlDbType = SqlDbType.NVarChar;
                paramlist.Add(sqlMonth);
                SalaryDetailsById = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_EmpGetSalaryDetailsForMonth", paramlist);

            }
            catch (Exception EX)
            {

                throw EX;
            }

            return SalaryDetailsById;
        }

        public DataTable EmpGetSalaryDetailsForMonthNewFormat(string EmployeeId, int month)
        {
            DataTable SalaryDetailsById = new DataTable();
            ArrayList paramlist = null;
            try
            {
                paramlist = new ArrayList();

                SqlParameter sqlEmpid = new SqlParameter();
                sqlEmpid.ParameterName = "@Employeeid";
                sqlEmpid.Value = EmployeeId.Trim();
                sqlEmpid.SqlDbType = SqlDbType.NVarChar;
                paramlist.Add(sqlEmpid);


                SqlParameter sqlMonth = new SqlParameter();
                sqlMonth.ParameterName = "@Month";
                sqlMonth.Value = month;
                sqlMonth.SqlDbType = SqlDbType.Int;
                paramlist.Add(sqlMonth);
                SalaryDetailsById = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_EmpGetSalaryDetailsForMonthNewFormat", paramlist);

            }
            catch (Exception EX)
            {

                throw EX;
            }

            return SalaryDetailsById;
        }
        public DataTable GetSalaryDetailsByMonthEmployeeNewFormat(string EmployeeId)
        {
            DataTable SalaryDetailsById = new DataTable();
            ArrayList paramlist = null;
            try
            {
                paramlist = new ArrayList();

                SqlParameter sqlEmpid = new SqlParameter();
                sqlEmpid.ParameterName = "@Employeeid";
                sqlEmpid.Value = EmployeeId;
                sqlEmpid.SqlDbType = SqlDbType.NVarChar;
                paramlist.Add(sqlEmpid);

                SalaryDetailsById = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetSalaryDetailsNewFormat", paramlist);

            }
            catch (Exception EX)
            {

                throw EX;
            }

            return SalaryDetailsById;
        }

        public object CheckExpenditureDateInTravelDesk(DateTime ExpenditureDate1, int? EmployeeID)
        {
            object Expendituredate = new object();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();

                SqlParameter spExpendituredate = new SqlParameter();
                spExpendituredate.ParameterName = "@ExpenditureDate";
                if (ExpenditureDate1 != null)
                {
                    spExpendituredate.Value = ExpenditureDate1;
                    spExpendituredate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spExpendituredate = null;
                    spExpendituredate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spExpendituredate);


                SqlParameter spEmployeeID = new SqlParameter();
                spEmployeeID.ParameterName = "@EmployeeID";
                if (EmployeeID != null)
                {
                    spEmployeeID.Value = EmployeeID;
                    spEmployeeID.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spEmployeeID.Value = null;
                    spEmployeeID.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(spEmployeeID);

                Expendituredate = GDSDBWrapper.ExecuteScalar("SP_ValidateExpenditureDateInTravelDesk", paramColl);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Expendituredate;
        }

        public DataTable GetEmployeeMonthlyExpenditures()
        {
            DataTable dtEmployeeAssets = new DataTable();
            ArrayList paramColl = null;
            try
            {
                paramColl = new ArrayList();
                dtEmployeeAssets = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetMonthlyEmployeeExpenditures", paramColl);
            }
            catch (Exception EX)
            {
                throw EX;
            }

            return dtEmployeeAssets;
        }

        public object GetSpanAmountForEmmployee(DateTime ExpenditureSDate, DateTime ExpenditureEDate, int EmpID)
        {
            ArrayList paramColl = null;
            object res = new object();
            try
            {
                paramColl = new ArrayList();

                #region empId
                SqlParameter paramusertype = new SqlParameter();
                paramusertype.ParameterName = "@empID";
                if (EmpID > 0)
                {
                    paramusertype.Value = EmpID;
                    paramusertype.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    paramusertype.Value = 0;
                    paramusertype.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(paramusertype);
                #endregion

                #region expSDate
                SqlParameter spMaxDate = new SqlParameter();
                spMaxDate.ParameterName = "@ExpSDate";
                if (ExpenditureSDate != null)
                {
                    spMaxDate.Value = ExpenditureSDate;
                    spMaxDate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spMaxDate.Value = null;
                    spMaxDate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spMaxDate);
                #endregion

                #region expEDate
                SqlParameter spEDate = new SqlParameter();
                spEDate.ParameterName = "@ExpEDate";
                if (ExpenditureEDate != null)
                {
                    spEDate.Value = ExpenditureEDate;
                    spEDate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spEDate.Value = null;
                    spEDate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spEDate);
                #endregion

                res = GDSDBWrapper.ExecuteScalar("SP_GetEmployeeSpanAmount", paramColl);
            }
            catch (Exception EX)
            {

                throw EX;
            }
            return res;
        }

        public DataTable GetEmployeeSpanRecords(DateTime ExpenditureSDate, DateTime ExpenditureEDate, string EmpID)
        {
            ArrayList paramColl = null;
            DataTable dtData = new DataTable();
            try
            {
                paramColl = new ArrayList();

                #region empId
                SqlParameter paramusertype = new SqlParameter();
                paramusertype.ParameterName = "@empID";
                if (Convert.ToInt32(EmpID) > 0)
                {
                    paramusertype.Value = EmpID;
                    paramusertype.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    paramusertype.Value = 0;
                    paramusertype.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(paramusertype);
                #endregion

                #region expSDate
                SqlParameter spMaxDate = new SqlParameter();
                spMaxDate.ParameterName = "@ExpSDate";
                if (ExpenditureSDate != null)
                {
                    spMaxDate.Value = ExpenditureSDate;
                    spMaxDate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spMaxDate.Value = null;
                    spMaxDate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spMaxDate);
                #endregion

                #region expEDate
                SqlParameter spEDate = new SqlParameter();
                spEDate.ParameterName = "@ExpEDate";
                if (ExpenditureEDate != null)
                {
                    spEDate.Value = ExpenditureEDate;
                    spEDate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spEDate.Value = null;
                    spEDate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spEDate);
                #endregion

                dtData = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetEmployeeSpanRecords", paramColl);
            }
            catch (Exception EX)
            {

                throw EX;
            }
            return dtData;
        }
        public int? InsertTravelUploadTicket(TravelUploadTicket trvelUpload)
        {
            ArrayList paramColl = null;
            int? result = null;
            try
            {
                paramColl = new ArrayList();

                #region parameters

                SqlParameter spId = new SqlParameter();
                spId.ParameterName = "@TravelId";
                spId.Value = trvelUpload.Fk_TravelId;
                spId.SqlDbType = SqlDbType.Int;
                paramColl.Add(spId);

                SqlParameter spImagePath = new SqlParameter();
                spImagePath.ParameterName = "@TravelUploadImagePath";
                spImagePath.Value = trvelUpload.TicketImage;
                spImagePath.SqlDbType = SqlDbType.NVarChar;
                paramColl.Add(spImagePath);
                #endregion

                GDSDBWrapper.ExecuteNonQueryOutParam("SP_InsertTravelTicket", paramColl);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }
        public int? DeleteTravelTicket(TravelUploadTicket travelUploadTicket)
        {
            int travelMulti = 0;
            ArrayList paramlist = null;
            try
            {
                paramlist = new ArrayList();
                SqlParameter spdelete = new SqlParameter();
                spdelete.ParameterName = "@Fk_TravelId";
                if (travelUploadTicket.Fk_TravelId != null)
                {
                    spdelete.Value = travelUploadTicket.Fk_TravelId;
                    spdelete.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spdelete.Value = null;
                    spdelete.SqlDbType = SqlDbType.Int;
                }
                paramlist.Add(spdelete);

                SqlParameter sptravelimage = new SqlParameter();
                sptravelimage.ParameterName = "@TicketImage";
                if (travelUploadTicket.TicketImage != null)
                {
                    sptravelimage.Value = travelUploadTicket.TicketImage;
                    sptravelimage.SqlDbType = SqlDbType.NVarChar;
                }
                else
                {
                    sptravelimage.Value = null;
                    sptravelimage.SqlDbType = SqlDbType.NVarChar;
                }
                paramlist.Add(sptravelimage);

                travelMulti = GDSDBWrapper.ExecuteNonQueryOutParam("SP_DeleteTravelTicket", paramlist);
            }
            catch (Exception)
            {

                throw;
            }
            return travelMulti;
        }
        public DataTable GetTravelTicketsListById(int? id)
        {
            DataTable dtable = new DataTable();
            ArrayList paramlist = null;
            try
            {
                paramlist = new ArrayList();
                SqlParameter spTravelid = new SqlParameter();
                spTravelid.ParameterName = "@TravelId";
                if (id != null)
                {
                    spTravelid.Value = id;
                    spTravelid.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    spTravelid.Value = null;
                    spTravelid.SqlDbType = SqlDbType.Int;
                }
                paramlist.Add(spTravelid);

                dtable = GDSDBWrapper.ExecuteDataReaderMultiParam("SP_GetTravelTicketsListById", paramlist);
            }
            catch (Exception)
            {

                throw;
            }
            return dtable;
        }

        public object GetEmployeeExpenditureCountInSpanDates(int? EmpID, string StartDate, string EndDate)
        {
            ArrayList paramColl = null;
            object res = new object();
            try
            {
                paramColl = new ArrayList();

                #region empId
                SqlParameter paramusertype = new SqlParameter();
                paramusertype.ParameterName = "@empID";
                if (EmpID > 0)
                {
                    paramusertype.Value = EmpID;
                    paramusertype.SqlDbType = SqlDbType.Int;
                }
                else
                {
                    paramusertype.Value = 0;
                    paramusertype.SqlDbType = SqlDbType.Int;
                }
                paramColl.Add(paramusertype);
                #endregion

                #region expSDate
                SqlParameter spMaxDate = new SqlParameter();
                spMaxDate.ParameterName = "@ExpSDate";
                if (StartDate != null)
                {
                    spMaxDate.Value = StartDate;
                    spMaxDate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spMaxDate.Value = null;
                    spMaxDate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spMaxDate);
                #endregion

                #region expEDate
                SqlParameter spEDate = new SqlParameter();
                spEDate.ParameterName = "@ExpEDate";
                if (EndDate != null)
                {
                    spEDate.Value = EndDate;
                    spEDate.SqlDbType = SqlDbType.DateTime;
                }
                else
                {
                    spEDate.Value = null;
                    spEDate.SqlDbType = SqlDbType.DateTime;
                }
                paramColl.Add(spEDate);
                #endregion

                res = GDSDBWrapper.ExecuteScalar("SP_GetEmployeeExpenditureCountInSpanDates", paramColl);
            }
            catch (Exception EX)
            {

                throw EX;
            }
            return res;
        }
    }
}
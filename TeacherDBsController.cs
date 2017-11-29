﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace TouchPoint
{
    public class TeacherDBsController : ApiController
    {
        private TouchPointDBContext db = new TouchPointDBContext();

        // GET: api/TeacherDBs
        public IQueryable<TeacherDB> GetTeacherDB()
        {
            return db.TeacherDB;
        }

        // GET: api/TeacherDBs/5
        [ResponseType(typeof(TeacherDB))]
        public IHttpActionResult GetTeacherDB(int id)
        {
            TeacherDB teacherDB = db.TeacherDB.Find(id);
            if (teacherDB == null)
            {
                return NotFound();
            }

            return Ok(teacherDB);
        }

        // PUT: api/TeacherDBs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeacherDB(int id, TeacherDB teacherDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teacherDB.Teacher_ID)
            {
                return BadRequest();
            }

            db.Entry(teacherDB).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherDBExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TeacherDBs
        [ResponseType(typeof(TeacherDB))]
        public IHttpActionResult PostTeacherDB(TeacherDB teacherDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TeacherDB.Add(teacherDB);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TeacherDBExists(teacherDB.Teacher_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = teacherDB.Teacher_ID }, teacherDB);
        }

        // DELETE: api/TeacherDBs/5
        [ResponseType(typeof(TeacherDB))]
        public IHttpActionResult DeleteTeacherDB(int id)
        {
            TeacherDB teacherDB = db.TeacherDB.Find(id);
            if (teacherDB == null)
            {
                return NotFound();
            }

            db.TeacherDB.Remove(teacherDB);
            db.SaveChanges();

            return Ok(teacherDB);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeacherDBExists(int id)
        {
            return db.TeacherDB.Count(e => e.Teacher_ID == id) > 0;
        }
    }
}
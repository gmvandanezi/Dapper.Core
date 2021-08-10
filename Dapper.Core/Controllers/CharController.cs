using Dapper.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Core.Controllers
{
    public class CharController : Controller
    {
        private readonly string ConnectionString = @"Password=sa123456;Persist Security Info=True;User ID=sa;Initial Catalog=GameWorldDapper;Data Source=DESKTOP-9KM479N\SQLEXPRESS";
        public IActionResult Index()
        {
            IDbConnection con;

            try
            {
                string query = "SELECT * FROM Characters ORDER BY Nome";
                con = new SqlConnection(ConnectionString);
                con.Open();
                IEnumerable<Character> personagens = con.Query<Character>(query).ToList();
                con.Close();
                return View(personagens);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Character player)
        {
            if (ModelState.IsValid)
            {
                IDbConnection con;

                try
                {
                    string query = "INSERT INTO Characters (Id, Nome, Gender, Vocacao) VALUES (@id, @nome, @gender, @vocacao)";
                    con = new SqlConnection(ConnectionString);
                    con.Open();
                    con.Execute(query, new {
                        id = player.Id,
                        nome = player.Nome,
                        gender = player.Gender.ToString(),
                        vocacao = player.Vocacao.ToString()
                    });
                    con.Close();
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return View(player);        
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            IDbConnection con;
            try
            {
                string q = "SELECT * FROM Characters WHERE Id = @id";
                con = new SqlConnection(ConnectionString);
                con.Open();
                Character player = con.QueryFirstOrDefault<Character>(q, new { id = id });
                con.Close();
                return View(player);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public IActionResult Edit(string id, Character player)
        {
            if (id != player.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                IDbConnection con;

                try
                {
                    con = new SqlConnection(ConnectionString);
                    string q = "UPDATE Characters SET Nome=@nome, Gender=@gender, Vocacao=@vocacao WHERE Id=@id";
                    con.Open();
                    con.Execute(q, new
                    {
                        id = player.Id,
                        nome = player.Nome,
                        gender = player.Gender.ToString(),
                        vocacao = player.Vocacao.ToString()
                    });
                    con.Close();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return View(player);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            IDbConnection con;
            try
            {
                string q = "SELECT * FROM Characters WHERE Id = @id";
                con = new SqlConnection(ConnectionString);
                con.Open();
                Character player = con.QueryFirstOrDefault<Character>(q, new { id = id });
                con.Close();
                return View(player);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public IActionResult Delete(string id, Character player)
        {
                IDbConnection con;

                try
                {
                    con = new SqlConnection(ConnectionString);
                    string q = "DELETE FROM Characters WHERE Id=@id";
                    con.Open();
                    con.Execute(q, new { id = id });
                    con.Close();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    throw ex;
                }
        }
    }
}


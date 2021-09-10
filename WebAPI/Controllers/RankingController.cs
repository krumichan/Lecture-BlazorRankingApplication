using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    // REST (Representational State Transfer)
    // 공식 표준 스펙은 아님.
    // 원래 있던 HTTP 통신에서 기능을 '재사용'하여
    // 데이터 송수신 규칙을 만든 것.

    // CRUD
    // www.naver.com -> 백화점 지하 1층
    // www.naver.com/sports/ -> 백화점 지하 1층 일식당 xxx
    // verb ( GET POST PUT ... )

    // CREATE
    // POST /api/ranking
    // -- 아이템 생성 요청 ( Body에 실제 정보 삽입 )

    // READ
    // GET /api/ranking
    // 모든 item 호출.
    // GET /api/ranking/1
    // id=1인 item 호출.

    // UPDATE
    // PUT /api/ranking ( PUT은 보안 문제로 web에서는 사용하지 않음. )
    // item 갱신 요청 (Body에 실제 정보)

    // DELETE
    // DELETE /api/ranking/1 ( DELETE는 보안 문제로 web에서는 사용하지 않음. )
    // id=1인 item 삭제.

    // API Controller 특징.
    // 1. C# 객체를 반환해도 된다.
    //   -- null 반환 -> 클라에 204 Response (No Content)
    //   -- string  반환 -> text/plain
    //   -- 나머지(int, bool) 반환 -> application/json

    [Route("api/[controller]")]
    [ApiController]
    public class RankingController : ControllerBase
    {
        ApplicationDbContext _context;

        public RankingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create
        [HttpPost]
        public GameResult AddGameResult([FromBody] GameResult gameResult)
        {
            _context.GameResults.Add(gameResult);
            _context.SaveChanges();

            return gameResult;
        }

        // Read
        [HttpGet]
        public List<GameResult> GetGameResults()
        {
            List<GameResult> results = _context.GameResults
                .OrderByDescending(item => item.Score)
                .ToList();

            // aplication/json 반환.
            return results;
        }

        [HttpGet("{id}")]
        public GameResult GetGameResult(int id)
        {
            GameResult result = _context.GameResults
                .Where(item => item.Id == id)
                .FirstOrDefault();

            // aplication/json 반환.
            return result;
        }

        // Update
        [HttpPut]
        public bool UpdateGameResult([FromBody] GameResult gameResult)
        {
            var find = _context.GameResults
                .Where(x => x.Id == gameResult.Id)
                .FirstOrDefault();

            if (find == null)
            {
                return false;
            }

            find.UserName = gameResult.UserName;
            find.Score = gameResult.Score;
            _context.SaveChanges();

            return true;
        }

        // Delete
        [HttpDelete("{id}")]
        public bool DeleteGameObject(int id)
        {
            var find = _context.GameResults
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (find == null)
            {
                return false;
            }

            _context.GameResults.Remove(find);
            _context.SaveChanges();

            return true;
        }
    }
}

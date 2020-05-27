using NGO.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NGO.Models.DAO
{
    public class UserQuestionDAO
    {
        public static bool CreateQues(UserQuestion q)
        {
            NGOEntities e = new NGOEntities();
            q.QuesDateCreate = DateTime.Now;
            q.QuesNew = true;
            e.UserQuestions.Add(q);
            if (e.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static UserQuestion GetQuesByID(int id)
        {
            NGOEntities e = new NGOEntities();
            return e.UserQuestions.Find(id);
        }
        public static List<UserQuestion> GetQuesNew()
        {
            NGOEntities e = new NGOEntities();
            return e.UserQuestions.Where(w => w.QuesNew == true).OrderBy(o => o.QuesDateCreate).ToList();
        }
        public static bool DelQues(int id)
        {
            NGOEntities e = new NGOEntities();
            var item = e.UserQuestions.Find(id);
            e.UserQuestions.Remove(item);
            if (e.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public static bool InsertAns(int id,string answer)
        {
            NGOEntities e = new NGOEntities();
            var item = e.UserQuestions.Find(id);
            item.AnswerContent = answer;
            item.AnswerDateCreate = DateTime.Now;
            item.QuesNew = false;
            if (e.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
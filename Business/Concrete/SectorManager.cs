using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SectorManager : ISectorService
    {
        ISectorDal _sectorDal;

        public SectorManager(ISectorDal sectorDal)
        {
            _sectorDal = sectorDal;
        }

        [CacheRemoveAspect("ISectorService.Get")]
        public async Task<IResult> AsyncAdd(Sector t)
        {
            var result = await _sectorDal.AsyncAddDB(t);
            return result
                ? new SuccessResult(Messages.Added)
                : new ErrorResult(Messages.NotAdded);
        }

        [CacheAspect]
        public async Task<IDataResult<List<Sector>>> AsyncGetAll()
        {
            var result = await _sectorDal.AsyncGetAllDB();
            return result != null
                ? new SuccessDataResult<List<Sector>>(result)
                : new ErrorDataResult<List<Sector>>(Messages.Error);
        }

        public async Task<IDataResult<Sector>> AsyncGetById(int id)
        {
            var result = await _sectorDal.AsyncGetDB(p => p.Id == id);
            return result != null
                ? new SuccessDataResult<Sector>(result)
                : new ErrorDataResult<Sector>(Messages.Error);
        }

        [CacheRemoveAspect("ISectorService.Get")]
        [TransactionAspect]
        public async Task<IResult> AsyncRemove(Sector t)
        {
            var result = await _sectorDal.AsyncDeleteDB(t);
            return result
                ? new SuccessResult(Messages.Removed)
                : new ErrorResult(Messages.NotRemoved);
        }

        [CacheRemoveAspect("ISectorService.Get")]
        public async Task<IResult> AsyncUpdate(Sector t)
        {
            var result = await _sectorDal.AsyncUpdateDB(t);
            return result
                ? new SuccessResult(Messages.Updated)
                : new ErrorResult(Messages.NotUpdated);
        }

        [CacheRemoveAspect("ISectorService.Get")]
        public async Task<IResult> RemoveById(int id)
        {
            var sector = await AsyncGetById(id);
            var result = await AsyncRemove(sector.Data);
            return result.Success
                ? new SuccessResult()
                : new ErrorResult();
        }

        [CacheAspect]
        public IDataResult<List<Person>> GetFirmGroupBySector()
        {
            var result = _sectorDal.GetFirmGroupBySector();
            return result != null
                ? new SuccessDataResult<List<Person>>(result)
                : new ErrorDataResult<List<Person>>(Messages.Error);
        }

        [CacheAspect]
        public IDataResult<List<Person>> GetFirmGroupBySectorBySectorId(int sectorId)
        {
            var result = _sectorDal.GetFirmGroupBySectorBySectorId(sectorId);
            return result != null
                ? new SuccessDataResult<List<Person>>(result)
                : new ErrorDataResult<List<Person>>(Messages.Error);
        }
    }
}

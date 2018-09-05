# Copyright (c) 2017, Udacity
# All rights reserved.
#
# Redistribution and use in source and binary forms, with or without
# modification, are permitted provided that the following conditions are met:
#
# 1. Redistributions of source code must retain the above copyright notice, this
#    list of conditions and the following disclaimer.
# 2. Redistributions in binary form must reproduce the above copyright notice,
#    this list of conditions and the following disclaimer in the documentation
#    and/or other materials provided with the distribution.
#
# THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
# ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
# WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
# DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
# ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
# (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
# LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
# ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
# (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
# SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#
# The views and conclusions contained in the software and documentation are those
# of the authors and should not be interpreted as representing official policies,
# either expressed or implied, of the FreeBSD Project

# Author: Devin Anzelmo

import glob
import os
import shutil
import sys
import argparse

import numpy as np
from scipy import misc
import tqdm

def make_dir_if_not_exist(path):
    if not os.path.exists(path):
        os.makedirs(path)


def get_mask_files(image_files):
    is_cam2 = lambda x: x.find('cam2_') != -1
    is_cam3 = lambda x: x.find('cam3_') != -1
    is_cam4 = lambda x: x.find('cam4_') != -1

    cam2 = sorted(list(filter(is_cam2, image_files)))
    cam3 = sorted(list(filter(is_cam3, image_files)))
    cam4 = sorted(list(filter(is_cam4, image_files)))
    return cam2, cam3, cam4


def move_labels(input_folder, output_folder, fold_id):
    files = glob.glob(os.path.join(input_folder, '*.png'))

    output_folder = os.path.join(output_folder, 'masks')
    make_dir_if_not_exist(output_folder)
    cam2, cam3, cam4 = get_mask_files(files) 

    for e,i in tqdm.tqdm( enumerate(cam2), desc = 'moving labels', leave = False ):
        fname_parts = i.split(os.sep)

        # Thanks @Nitish for these fixes:)
        cam2_base = str(fold_id) + '_' + fname_parts[-3] +'_' + fname_parts[-1]

        fname_parts = cam3[e].split(os.sep)
        cam3_base = str(fold_id) + '_' + fname_parts[-3] +'_' + fname_parts[-1]

        fname_parts = cam4[e].split(os.sep)
        cam4_base = str(fold_id) + '_' + fname_parts[-3] +'_' + fname_parts[-1]

        shutil.copy(i, os.path.join(output_folder,cam2_base))
        shutil.copy(cam3[e], os.path.join(output_folder,cam3_base))
        shutil.copy(cam4[e], os.path.join(output_folder,cam4_base))


def move_png_to_jpeg(input_folder, output_folder, fold_id):
    files = glob.glob(os.path.join(input_folder, '*.png'))
    is_cam1 = lambda x: x.find('cam1_') != -1
    cam1_files = sorted(list(filter(is_cam1, files)))
    output_folder = os.path.join(output_folder, 'images')
    make_dir_if_not_exist(output_folder)

    for i in tqdm.tqdm( cam1_files, desc = 'png2jpeg', leave = False ):
        cam1 = misc.imread(i)
        fname_parts = i.split(os.sep)
        cam1_base = str(fold_id) + '_' +fname_parts[-3] + '_' + fname_parts[-1].split('.')[0] + '.jpeg'
        misc.imsave(os.path.join(output_folder, cam1_base), cam1, format='jpeg')


def combine_masks(processed_folder):
    processed_mask_folder = os.path.join(processed_folder, 'masks')
    files = glob.glob(os.path.join(processed_mask_folder, '*.png'))
    cam2, cam3, cam4 = get_mask_files(files)

    for e,i in tqdm.tqdm( enumerate(cam2), desc = 'masks', leave = False ):
        im2 = misc.imread(i)[:,:,0]
        im3 = misc.imread(cam3[e])[:,:,0]
        im4 = misc.imread(cam4[e])[:,:,0]

        stacked = np.stack((im4-1, im2, im3), 2)
        argmin = np.argmin(stacked, axis=-1)
        im = np.stack((argmin==0, argmin==1, argmin==2), 2)

        base_name = os.path.basename(i)
        ind = base_name.find('cam')
        new_fname = base_name[:ind] + 'mask'+ base_name[ind+4:]

        dir_base = str(os.sep).join(i.split(str(os.sep))[:-1])
        im = im.astype( np.int32 )
        misc.imsave(os.path.join(dir_base, new_fname), im)
        os.remove(i)
        os.remove(cam3[e])
        os.remove(cam4[e])


def get_im_data( targetFolder ):
    indicator_dict = dict()
    
    files = glob.glob( os.path.join( targetFolder, '*.png' ) )
    if len( files ) == 0:
        indicator_dict[targetFolder] = False
    else:
        indicator_dict[targetFolder] = True

    return indicator_dict

def init( mode, rootFolder, singleFolder, joinFolder ) : 
    
    if mode == 'SINGLE' :
        _targetFolder = os.path.join( rootFolder, singleFolder )
        _resultsFolder = os.path.join( rootFolder, singleFolder + '_results' )
        _initSingleMode( _targetFolder, _resultsFolder )

    elif mode == 'GROUP' :
        _initGroupMode( rootFolder )

    elif mode == 'JOIN' :
        _initJoinMode( rootFolder, joinFolder )

    else :
        print( 'Ops, something went wrong' )
        sys.exit( -1 )

def _initSingleMode( targetFolder, resultsFolder ) : 
    print( 'Initializing SINGLE mode' )
    _processFolder( targetFolder, resultsFolder )

def _initGroupMode( rootFolder ) :
    print( 'Initializing GROUP mode' )
    # extract folders to process
    _folders = []
    for ( dirpath, dirnames, filenames ) in os.walk( rootFolder ) :
        _folders.extend( dirnames )
        break
    # process the folders each at a time
    for i in tqdm.tqdm( range( len( _folders ) ) ):
        _processFolder( os.path.join( rootFolder, _folders[i] ),
                        os.path.join( rootFolder, _folders[i] + '_results' ) )

def _initJoinMode( rootFolder, joinFolder ) :
    print( 'Initializing JOIN mode' )
    # just in case
    make_dir_if_not_exist( os.path.join( rootFolder, joinFolder ) )
    # first, check all folders
    _folders = []
    for ( dirpath, dirnames, filenames ) in os.walk( rootFolder ) :
        _folders.extend( dirnames )
        break
    # copy the contents from each folder to the joinFolder
    for _folder in tqdm.tqdm( _folders, desc = 'copying folders contents' ) :
        # make src reference folders
        _srcImagesFolder = os.path.join( rootFolder, _folder, 'images' )
        _srcMasksFolder = os.path.join( rootFolder, _folder, 'masks' )
        # make dst reference folders
        _dstImagesFolder = os.path.join( rootFolder, joinFolder, 'images' )
        _dstMasksFolder = os.path.join( rootFolder, joinFolder, 'masks' )
        make_dir_if_not_exist( _dstImagesFolder )
        make_dir_if_not_exist( _dstMasksFolder )
        # get files
        _filesImages = glob.glob( os.path.join( _srcImagesFolder, '*.jpeg' ) )
        _filesMasks = glob.glob( os.path.join( _srcMasksFolder, '*.png' ) )
        # move them
        for _file in _filesImages :
            try :
                shutil.move( _file, _dstImagesFolder )
            except:
                print( 'duplicate> ', _file )
        for _file in _filesMasks :
            try :
                shutil.move( _file, _dstMasksFolder )
            except :
                print( 'duplicate> ', _file )

def _processFolder( targetFolder, resultsFolder ) :
    make_dir_if_not_exist( resultsFolder )
    indicator_dict = get_im_data( targetFolder ) 

    print( 'target folder> ', targetFolder )
    print( 'results folder> ', resultsFolder )
    print( 'dict> ', indicator_dict )

    for e, i in enumerate(indicator_dict.items()):
        # no data in the folder so skip it
        if not i[1]:
            continue

        move_png_to_jpeg(i[0], resultsFolder, e)
        move_labels(i[0], resultsFolder, e)

    combine_masks(resultsFolder)


if __name__ == '__main__':
    # make parser
    _parser = argparse.ArgumentParser( description = 'Image preprocessing tools.' )
    _parser.add_argument( 'root', type = str, 
                          help = 'base working folder' )
    _parser.add_argument( 'mode', type = str, 
                          help = 'whether to work with a ' + 
                                 '(SINGLE) folder ' + 
                                 '(GROUP) of folders, or ' + 
                                 '(JOIN) to combine everything into a single batch' )
    _parser.add_argument( '--singleFolder', type = str,
                          help = 'if mode is single, use this folder as the image container',
                          default = 'raw_sim_data' )
    _parser.add_argument( '--joinFolder', type = str,
                          help = 'if mode is join, use this folder as place to combine all results',
                          default = 'train' )
    _args = _parser.parse_args()
    _argRoot = _args.root
    _argMode = ( _args.mode if ( _args.mode == 'SINGLE' or 
                                 _args.mode == 'GROUP' or
                                 _args.mode == 'JOIN' ) else 'SINGLE' )

    print( 'WORKING DIRECTORY : ', _argRoot )
    print( 'MODE : ', _argMode )

    init( _argMode, _argRoot, _args.singleFolder, _args.joinFolder )
